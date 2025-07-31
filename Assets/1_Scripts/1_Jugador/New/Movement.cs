using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;

public class Movement : MonoBehaviour
{
    #region Atributes
    [Header("Importaciones De Scripts.")]
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    private Jump jump;
    
    [Space]
    [Header("SaltoDinamico")]
    [Range(0, 1)] [SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;
    private bool botonSalto = true;
    private bool saltar;

    [Space]
    [Header("Archivos De Lectura.")]
    [SerializeField] private float speed = 3;
    [SerializeField] private float fuerzaSalto = 5f;
    
    [Space]
    [Header("ControlSetter")]
    [SerializeField] private MenuPausa mp;
    [SerializeField] private GameObject buttons;
    public int tecladoOMovil = 1;//Logica Para Teclado O Movil.
    private bool playing = true;//Logica De SFX.
    private float horizontal;//Variable Para Moverse.
    private float _fixedHorizontal;//Variable Para Moverse.
    
    
    [Space]
    [Header("Raycast")]
    private bool _isGrounded;
    private bool _isOnLWall;
    private bool _isOnRWall;
    private bool _step;
    private float _descendVelocity = .25f;
    
    [Space]
    [Header("Logica")]
    private bool _jumpTurnBlocker = false;
    private bool alive = true;
    private bool muteSteps = false;
    
#endregion
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jump = GetComponent<Jump>();
    }
    void Start()
    {
        LoadData();//Cargar Datos.
#if UNITY_EDITOR
        tecladoOMovil = 1;
#elif UNITY_STANDALONE_WIN
            tecladoOMovil = 1;
#elif UNITY_WEBGL
            tecladoOMovil = 2;
#elif UNITY_ANDROID
            tecladoOMovil = 2;
#elif UNITY_IOS
            tecladoOMovil = 2;
#endif
    }
    void Update()
    {
        #region Raycast Ground & Ground
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Raycast Ground & Ground
        if (Physics2D.Raycast(rb.position - new Vector2(0.3f, 0.1f), Vector2.down, .25f, LayerMask.GetMask("Ground")) 
            || (Physics2D.Raycast(rb.position + new Vector2(0.3f, -0.1f), Vector2.down, .25f, LayerMask.GetMask("Ground"))))
            _isGrounded = true; else _isGrounded = false;

        if (Physics2D.Raycast(rb.position, Vector2.left, .25f, LayerMask.GetMask("Wall")))
            _isOnLWall = true; else _isOnLWall = false;
        
        if (Physics2D.Raycast(rb.position, Vector2.right, .25f, LayerMask.GetMask("Wall")))
            _isOnRWall = true; else _isOnRWall = false;
        
        if(!_isOnLWall && rb.velocity.y < 0 && Physics2D.Raycast(rb.position + new Vector2(0f,0.15f), Vector2.left, .1875f, LayerMask.GetMask("Wall")))
            rb.position = rb.position - new Vector2(0f, 0.165f);
        
        if(!_isOnRWall && rb.velocity.y < 0 && Physics2D.Raycast(rb.position + new Vector2(0f,0.15f), Vector2.right, .1875f, LayerMask.GetMask("Wall")))
            rb.position = rb.position - new Vector2(0f, 0.165f); 
        #endregion
        
        #region Wall's Slide
        /////////////////////////////////////////////////////////////////////////////////////////////
        /// Wall's Slide
        if (!_isGrounded && _isOnLWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -_descendVelocity, float.MaxValue));
        }
        if (!_isGrounded && _isOnRWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -_descendVelocity, float.MaxValue));
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region Animations

        if (!_isGrounded && _isOnLWall)
        {
            anim.Play("WallSlide");
            anim.SetBool("WallSlide", true);
            anim.SetFloat("SlideDirection", 0f);
            if (rb.velocity.y < 0)
            {
                anim.SetBool("WallSlide", false);
            }
        }

        if (!_isGrounded && _isOnRWall)
        {
            anim.Play("WallSlide");
            anim.SetBool("WallSlide", true);
            anim.SetFloat("SlideDirection", 1f);
            if (rb.velocity.y < 0)
            {
                anim.SetBool("WallSlide", false);
            }
        }

        if (!_isOnLWall && !_isOnRWall)
        {
            anim.SetBool("WallSlide", false);
        }
        #endregion
        
        #region Teclado O Movil
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Codigo para elegir entre usar teclado, flechas o joystick en movil:

        switch (tecladoOMovil)
        {
            case 1://teclado
                if (playing)
                {
                    horizontal = Input.GetAxisRaw("Horizontal");
                    buttons.SetActive(false);
                }
                break;
            case 2://movil
                if (playing)
                {
                    horizontal = mp.Gethorizontal();
                    buttons.SetActive(true);
                }
                break;
            default:
                Debug.LogWarning("Valor 'tecladoOMovil' no reconocido: " + tecladoOMovil);
                break;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        
        #region Movement
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Codigo Para Moverse

        if(alive){
            if (!_isOnLWall && horizontal < 0) _fixedHorizontal = -1;
            if (_isOnLWall && horizontal < 0) _fixedHorizontal = 0;
            if (!_isOnRWall && horizontal > 0) _fixedHorizontal = 1;
            if (_isOnRWall && horizontal > 0) _fixedHorizontal = 0;
            if (horizontal == 0) _fixedHorizontal = 0;
        }
        
        rb.velocity = new Vector2(_fixedHorizontal * speed, rb.velocity.y);//Codigo para moverse.
        
        if(_fixedHorizontal != 0) anim.SetFloat("MovState", 1f); else anim.SetFloat("MovState", 0);
        if(_fixedHorizontal == 1) anim.SetFloat("Direction", 1f);
        if(_fixedHorizontal == -1) anim.SetFloat("Direction", -1f);
        /////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }

    
    public void SetMuerte()//Desactiva el movimiento del jugador despues de morir.
    {
        alive = false;
        horizontal = 0;
        _fixedHorizontal = 0;
    }
    public void SetDamage()//Desactiva el movimiento del jugador despues de morir.
    {
        alive = false;
        muteSteps = true;
        horizontal = 0;
        _fixedHorizontal = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void SetDamageOff()//Desactiva el movimiento del jugador despues de morir.
    {
        alive = true;
        muteSteps = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void SaveData()//Guarda Los Datos Actuales.
    {
        SaveManager.SaveControlData(this);
    }
    private void LoadData()//Cargar La Ultima Configuracion De Controles.
    {
        PlayerData PlayerData = SaveManager.LoadControlData();
        tecladoOMovil = PlayerData.tecladoOMovil;
    }
    public void SetTeclado()//Metodo Para Establecer La Configuraci�n En Teclado.
    {
        tecladoOMovil = 1;
        SaveData();
    }
    public void SetMovil()//Metodo Para Establecer La Configuraci�n En Movil.
    {
        tecladoOMovil = 2;
        SaveData();
    }
    public void SetPlayOn()
    {
        playing = true;
    }
    public void SetPlayOff()
    {
        playing = false;
    }

    public bool StepsSFXState()
    {
        return muteSteps;
    }
}