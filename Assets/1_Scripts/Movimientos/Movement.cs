using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.XR;

public class Movement : MonoBehaviour
{
    [Header("Importaciones De Scripts.")]
    [SerializeField] FondoFollowY ff;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SFX sfx;
    [SerializeField] private GameObject buttons;
    [SerializeField] private MenuPausa mp;

    [Header("Archivos De Lectura.")]
    static private readonly float speed = 3;
    static private readonly float torque = 1f;
    static private readonly float fuerzaSalto = 5f;
    static private readonly float fuerzaSaltoTrampolin = 8f;

    [Header("Debug Squares.")]
    private bool Bug = false;
    private bool BugT = false;
    private bool BugPI = false;

    [Header("Logica General.")]
    private float horizontal;//Variable Para Moverse.
    private bool walking;//Logica De SFX.
    private bool playing = true;//Logica De SFX.
    public int tecladoOMovil = 1;//Logica Para Teclado O Movil.

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        LoadData();//Cargar Datos.
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, rb.velocity.y);//Devuelve La Velocidad Vertical Del Player.
    }

    void Update()
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //Codigos para desbugearse.
        if (Bug) transform.Translate(Vector3.up * .15f);
        if (BugT) transform.Translate(Vector3.down * .15f);
        if (BugPI) transform.Translate(Vector3.right * .15f);
        /////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////
        ///Codigo Para Saber Si Esta Cayendo O Subiendo.
        if (rb.velocity.y < 0)
        {
            anim.SetBool("Cayendo", true);
            anim.SetBool("Subiendo", false);
            anim.SetBool("Trampolin", false);
            anim.SetFloat("Caida", 1f);
            anim.SetBool("teclaJump", false);
        }
        else
        {
            anim.SetBool("Cayendo", false);
            anim.SetFloat("Caida", 0f);
        }
        if (rb.velocity.y > 0) { anim.SetBool("Subiendo", true); } else anim.SetBool("Subiendo", false);
        /////////////////////////////////////////////////////////////////////////////////////////////

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
            case 3:
                //Mamo jaja.
                break;
            default:
                Debug.LogWarning("Valor 'tecladoOMovil' no reconocido: " + tecladoOMovil);
                break;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Este Codigo Sirve Para Girar Al Personaje:
        if (horizontal < 0.0f)
        {
            if (rb.velocity.y == 0)
            {
                WalkingOn();
            }
            transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
        }
        else if (horizontal > 0.0f)
        {
            if (rb.velocity.y == 0) WalkingOn();
            transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Codigo Para Saltar:
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            anim.SetBool("teclaJump", true);
            sfx.PlayJump();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Codigos De Una Linea:
        anim.SetFloat("Corriendo", horizontal != 0.0f ? 1.0f : 0f);//Este Codigo Establece El Valor De La Variable "Corriendo" En 1 Si El Valor Horizontal Es Diferente De 0 Y En 0 Si Es Igual A 0.
        if (horizontal == 0 || rb.velocity.y != 0) WalkingOff();//Este codigo detiene el sonido de caminar si no se esta moviendo o esta en el aire.
        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);//Codigo para moverse, el valor horizontal se basa en el switch anterior.
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Codigo para el trampolin
        if (collision.tag == "Trampolin") { rb.AddForce(new Vector2(0f, fuerzaSaltoTrampolin), ForceMode2D.Impulse); anim.SetBool("Trampolin", true); sfx.PlayJump(); }
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Metodos Para Desbuggearse De La Pared.
        if (collision.tag == "Debug Square") transform.Translate(Vector3.up * .15f);
        if (collision.tag == "Debug Square Invertido") transform.Translate(Vector3.down * .15f);
        if (collision.tag == "Debug Square Pared Izq") transform.Translate(Vector3.right * .15f);
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Este Metodo Sirve Para Desbuggearse De La Pared.
        if (collision.tag == "Debug Square") Bug = true;
        if (collision.tag == "Debug Square Invertido") BugT = true;
        if (collision.tag == "Debug Square Pared Izq") BugPI = true;
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Este Metodo Sirve Para Activar El ******.
        if (collision.tag == "Trigger Wira")
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(torque);
            ff.SingWira();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///La Verdad No Se Para Que Sirve Este If.
        if (collision.tag == "ParedPrueba") Debug.Log("Contacto");
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Estos Metodos Sirven Para Desactivar El Desbuggeador De La Pared.
        if (collision.tag == "Debug Square") Bug = false;
        if (collision.tag == "Debug Square Invertido") BugT = false;
        if (collision.tag == "Debug Square Pared Izq") BugPI = false;
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void WalkingOn()//Reproduce El Sonido De Caminar.
    {
        if (walking == false)
        {
            sfx.PlayWalk();
            walking = true;
        }
    }
    private void WalkingOff()//Detiene El Sonido De Caminar.
    {
        if (walking == true)
        {
            sfx.StopWalk();
            walking = false;
        }
    }
    public void BotonJump()//Nota: Modificar este metodo para que funcione con el tiempo de pulsacion del boton. //Nota 2: Solo Funciona En Movil.
    {
        if (Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            sfx.PlayJump();
        }
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
    public void SetTeclado()//Metodo Para Establecer La Configuración En Teclado.
    {
        tecladoOMovil = 1;
        SaveData();
    }
    public void SetecladoOMovilovil()//Metodo Para Establecer La Configuración En Movil.
    {
        tecladoOMovil = 2;
        SaveData();
    }
    public void SetPlayOn()//Metodo Para SFX.
    {
        playing = true;
    }
    public void SetPlayOff()//Metodo Para SFX.
    {
        playing = false;
    }
    public void SetMuerte()//Desactiva el movimiento del jugador despues de morir.
    {
        tecladoOMovil = 3;
        horizontal = 0;
    }
}