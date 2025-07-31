using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    #region Atributes
    [Header("Importaciones De Scripts.")]
    [SerializeField] private Rigidbody2D rb;
    private SFXPlayer sfx;
    private Animator anim;
    
    [Space]
    [Header("SaltoDinamico")]
    [Range(0, 1)] [SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;
    private bool botonSalto = true;
    private bool saltar;
    
    [Space]
    [Header("Logica")]
    private bool _jumpTurnBlocker = false;
    private bool trampolin  = false;
    
    [Space]
    [Header("Raycast")]
    private bool _isGrounded;
    private bool _isOnLWall;
    private bool _isOnRWall;
    private bool _step;
    private float _descendVelocity = .25f;
    
    [Space]
    [Header("Archivos De Lectura.")]
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private float fuerzaSaltoTrampolin = 8f;
    [SerializeField] private float fuerzaSaltoTrampolin2 = 10f;
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<SFXPlayer>();
        escalaGravedad = rb.gravityScale;
        anim = GetComponent<Animator>();
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
        
        #region Salto Dinamico
        ///Codigo Para Saltar:
        if (Input.GetButton("Jump"))
        {
            ActionJump();
            AnimJump();
        }
        
        if (Input.GetButtonUp("Jump"))
        {
            SaltoDinamico();
        }
        #endregion

        
        
        
        if(rb.velocity.y == 0) _jumpTurnBlocker = false;
        
        
        
        if (_isGrounded) trampolin = false;
        
        if (!_isGrounded && !_isOnLWall && !_isOnRWall && rb.velocity.y < 0)
        {
            _jumpTurnBlocker = false;
            anim.Play("Jump");
            anim.SetFloat("JumpState", 1f);
        }
        
        if (!_jumpTurnBlocker && _isGrounded && !_isOnLWall && !_isOnRWall)
        {
            anim.SetBool("Jump", false);
        }
    }
    
    
    
    private void FixedUpdate()
    {
        if (saltar && botonSalto && (_isGrounded || _isOnLWall || _isOnRWall))
        {
            BotonJump();
        }

        if (rb.velocity.y < 0 && !_isGrounded)
        {
            rb.gravityScale = escalaGravedad * multiplicadorGravedad;
        }
        else
        {
            rb.gravityScale = escalaGravedad;
        }
    }
    
    
    
    
    public void ActionJump()
    {
        if (!_isOnLWall && !_isOnRWall && !trampolin)
        {
            saltar = true;
        }
    }
    public void SaltoDinamico()
    {
        if(!_isOnLWall && !_isOnRWall && !trampolin) {
            if (rb.velocity.y > 0f)
            {
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
            }
            botonSalto = true;
            saltar = false;
        }
    }
    
    
    
    
    
    
    public void AnimJump()
    {
        if(_isGrounded){
            _jumpTurnBlocker = true;
            anim.SetBool("Jump", true);
            anim.Play("Jump");
            anim.SetFloat("JumpState", 0f);
        }
    }
    public void BotonJump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.3f)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            sfx.JumpSFX();
        }
        saltar = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trampolin"))
        {
            AnimJump();
            trampolin = true;
            rb.AddForce(new Vector2(0f, fuerzaSaltoTrampolin), ForceMode2D.Impulse);
            sfx.JumpSFX();
            _jumpTurnBlocker = true;
        }
        
        if (collision.CompareTag("Trampolin2"))
        {
            AnimJump();
            trampolin = true;
            rb.AddForce(new Vector2(0f, fuerzaSaltoTrampolin2), ForceMode2D.Impulse);
            sfx.JumpSFX();
            _jumpTurnBlocker = true;
        }
    }
}
