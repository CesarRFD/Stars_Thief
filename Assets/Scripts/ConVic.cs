using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConVic : MonoBehaviour
{
    [SerializeField] private int puntos = 0;//Puntos del jugador
    [SerializeField] public bool CarryStar = false;//Victoria
    [SerializeField] private int puntosD = 0;//Si llega a 3 se pierde la partida
    [SerializeField] private bool Victoria = false;//Victoria
    [SerializeField] private bool Derrota = false;//Victoria
    //[SerializeField] private bool VictoriaPerreadora = false;//Victoria Perreadora*
    [SerializeField] private int puntosVictoria = 8;//Puntos necesarios para ganar
    [SerializeField] private int pVPerreadora = 10;//Puntos necesarios para Victoria Perreadora*

    [SerializeField] private bool CtoPuerta = false;//Estado de contacto con la puerta
    [SerializeField] private bool CtoEnemy = false;//Estado de contacto con un enemigo
    [SerializeField] private bool GodMode = false;//GodMode

    [SerializeField] private GameObject menuGameOver;

    private Rigidbody2D RB;
    private Animator anim;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckRadius = 0.25f;
    //[SerializeField] private float fuerzaImpulso = 1f;

    [SerializeField] private Puntaje puntaje;
    private bool Hit = false;

    [SerializeField] private Anim Anim;

    [SerializeField] private Enter Enter;
    [SerializeField] private GameObject btnenter;
    [SerializeField] private bool SetEnter;
    //[SerializeField] private bool SEPF = false;

    //[SerializeField] private Anim_Tra Anim_Tra;

    [SerializeField] private SFX sfx;
    private bool btn = false;

    private bool ValidarFalse = false;

    [SerializeField] private Image btn_izq;
    [SerializeField] private Image btn_der;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        btnenter.SetActive(false);
    }
    private void FixedUpdate()
    {
        //Debug.Log("FU");
        if (CarryStar && CtoEnemy)
        {
            PerderEstrellaT();
        }
        if (CarryStar && isGrounded)
        {
            PerderEstrellaT();
        }
    }
    private void Update()
    {
        /*if (SEPF)
        {
            Debug.Log("ExitConVic");
            SetEnterPuerta(false);
        }*/
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //Debug.Log("U");
        /*Hit = Anim.GetHitF();
        if (Puntaje.SumarPuntos() == 8)
        {
        }*/
        /*if (CarryStar && CtoPuerta)
        {
            //puntaje.SumarPuntos(cantidadPuntos);
            //Debug.Log("Punto agregado" + puntos);
        }*
        /*if (CarryStar == true && CtoEnemy == true)
        {
            anim.SetBool("Perder_Estrella", true);
            anim.SetBool("Perder_Estrella", false);
        }*/
        if (CarryStar == true && isGrounded)
        {
            sfx.PlayDaño();
            //Debug.Log("Tocaste espinas y perdiste la estrella que cargabas");
            CarryStar = false;
            //PerderEstrellaT();
            anim.SetBool("Perder_Estrella", true);
            puntosD++;
            GM(5);
            if (puntosD == 3)
            {
                //Debug.Log("Perdiste, ya no hay estrellas suficientes");
                puntosD = 0;
                btn_izq.enabled = false;
                btn_der.enabled = false;
                Derrota = true;
                CtoEnemy = false;
                sfx.PlayGG();
                menuGameOver.SetActive(true);
            }
        }
        /*if(ValidarFalse)
        {
            PerderEstrellaF();
        }*/
        if (CarryStar == false && isGrounded && GodMode == false)
        {
            sfx.PlayDaño();
            //Debug.Log("Perdiste");
            puntosD = 0;
            btn_izq.enabled = false;
            btn_der.enabled = false;
            Derrota = true;
            sfx.PlayGG();
            menuGameOver.SetActive(true);
        }
        if (puntos == 8)
        {
            anim.SetBool("Abrir Puerta", true);
        }
        if (Input.GetKeyDown(KeyCode.Return) && puntos >= pVPerreadora && CtoPuerta)
        {
            //VictoriaPerreadora = true;
            //Debug.Log("Victoria Perreadora");
        }
        if ((btn==true || Input.GetKeyDown(KeyCode.Return)) && puntos >= puntosVictoria && puntos < pVPerreadora && CtoPuerta)
        {
            //sfx.PlayGG();
            //Victoria = true;
            //Debug.Log("Victoria");
            StartCoroutine(EsperarGG(1.5f));
        }
        if (Victoria)
        {
            
        
            //StartCoroutine(EsperarGG(1));
            
        
            
            //SceneManager.LoadScene("Victory");
        }

        if (CarryStar == true && CtoEnemy == true)
        {
            sfx.PlayDaño();
            //Debug.Log("Te Toco Un Enemigo y perdiste la estrella que cargabas");
            CarryStar = false;
            anim.SetBool("Perder_Estrella", true);
            //PerderEstrellaT();
            puntosD++;
            GM(5);
            if (puntosD == 3)
            {
                //Debug.Log("Perdiste, ya no hay estrellas suficientes");
                puntosD = 0;
                btn_izq.enabled = false;
                btn_der.enabled = false;
                Derrota = true;
                CtoEnemy = false;
                sfx.PlayGG();
                menuGameOver.SetActive(true);
            }
        }
        if (CarryStar == false && CtoEnemy == true && GodMode == false)
        {
            sfx.PlayDaño();
            //Debug.Log("Perdiste");
            puntosD = 0;
            btn_izq.enabled = false;
            btn_der.enabled = false;
            Derrota = true;
            sfx.PlayGG();
            menuGameOver.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Star")
        {
            CarryStar = true;
            //Debug.Log("Cargando estrella*");
        }
        if (collision.tag == "Puerta")
        {
            if (puntos >= 8)
            {
                SetEnterPuerta(true);
                btnenter.SetActive(true);
                //SEPF = false;
            }
            if (CarryStar == true)
            {
                puntos++;
                Hit = true;
                //puntaje.SumarPuntos(puntos);
                sfx.PlayGame();
                CarryStar = false;
            }
            CtoPuerta = true;
        }
        if (collision.tag == "Enemy")
        {
            //RB.AddForce(new Vector2(0f, fuerzaImpulso), ForceMode2D.Impulse);
            CtoEnemy = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Puerta"))
        {
            //Debug.Log("Los objetos ya no están en contacto.");
            //SEPF = true;
            SetEnterPuerta(false);
            CtoPuerta = false;
            btnenter.SetActive(false);
        }
        if (collision.CompareTag("Enemy"))
        {
            CtoEnemy = false;
        }
    }
    private void GM(float Time)
    {
        GodMode = true;
        //Debug.Log("GodMode Activado");
        StartCoroutine(Esperar(3));
    }
    IEnumerator Esperar(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        GodMode = false;
        anim.SetBool("Perder_Estrella", false);
        Debug.Log("Termina_Anim");
        //Debug.Log("GodMode Desactivado");
        //Debug.Log("Waited for " + Tiempo + " seconds");
    }
    public int GetPuntos()
    {
        return puntos;
    }
    public bool GetCarryStar()
    {
        return CarryStar;
    }
    /*public bool GetHit()
    {
        return Hit;
    }*/
    private void PerderEstrellaT()
    {
        Debug.Log("Inicia_Anim");
        anim.SetBool("Perder_Estrella", true);
        Debug.Log("Inicia_Conteo" + Esperar(.5f));
        StartCoroutine(Esperar(.5f));
        /*anim.SetBool("Perder_Estrella", true);
        ValidarFalse = true;*/
    }
    /*private void PerderEstrellaF()
    {
        ValidarFalse = true;
        anim.SetBool("Perder_Estrella", false);
    }*/
    private void SetEnterPuerta(bool SetCtoPuerta)
    {
        if (SetCtoPuerta)
        {
            //Debug.Log("Set True");
            SetEnter = true;
        }
        else
        {
            //Debug.Log("Set False");
            SetEnter = false;
        }
    }
    public bool GetEnter()
    {
        Debug.Log("El GetEnter es: " + SetEnter);
        return SetEnter;
    }
    IEnumerator EsperarGG(float Tiempo)
    {
        sfx.PlayGame();
        yield return new WaitForSeconds(Tiempo);
        SceneManager.LoadScene("Victory");
    }
    public void fnbtn()
    {
        btn = true;
        StartCoroutine(EsperarBtn(.3f));
    }
    IEnumerator EsperarBtn(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        btn = false;
    }
}