using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Image = UnityEngine.UI.Image;

public class ConVic : MonoBehaviour
{
    [Header("Herramientas.")]
    [SerializeField] private bool godMode = false;//GodMode
    [SerializeField] public bool cargandoEstrella = false;//Indica si el jugador esta cargando una estrella.
    [SerializeField] private int puntuacionPlayer = 0;//Puntuación del jugador.
    [SerializeField] private int puntuacionParaDerrota = 0;//Indica la puntuación para perder.

    [Header("Importaciones De Scripts.")]
    [SerializeField] private Movement mov;
    [SerializeField] private Puntaje puntaje;
    [SerializeField] private Enter Enter;
    [SerializeField] private SFX sfx;

    [Header("Importaciones De GameObjects.")]
    [SerializeField] private GameObject botonEnter;
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private Image btnPause;
    private Animator anim;

    [Header("Importaciones De Imagenes.")]
    [SerializeField] private UnityEngine.UI.Image btn_izq;
    [SerializeField] private UnityEngine.UI.Image btn_der;
    [SerializeField] private UnityEngine.UI.Image btn_salto;

    [Header("Variables.")]
    private bool SetEnter;//Variable que activa el boton tactil Enter y/o el cartel de "Pulsa Enter".
    static private readonly int puntosVictoria = 8;//Puntos necesarios para ganar.
    private bool contactoConPuerta = false;//Estado de contacto con la puerta.
    private bool invulnerabilityTime = false;//Invulnerabilidad temporal del jugador al perder una estrella.
    private bool botonEnterCelular = false;//Boton tactil Enter.
    private bool alreadyDead = false;//Indica si el jugador ya murio.

    private void Awake()
    {
        anim = GetComponent<Animator>();
        botonEnter.SetActive(false);
    }
    private void Update()
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        ///Si el jugador tiene 8 estrellas, se activa la animación de abrir la puerta.
        if (puntuacionPlayer == 8) anim.SetBool("Abrir Puerta", true);
        //////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////
        ///Si la puntuación del jugador es mayor o igual a 8, tiene contacto con la puerta y presiona
        ///la tecla Enter o el botón de la pantalla táctil, se activa la victoria.
        if (puntuacionPlayer >= puntosVictoria && contactoConPuerta && (botonEnterCelular || Input.GetKeyDown(KeyCode.Return))) StartCoroutine(Victory(1.5f));
        //////////////////////////////////////////////////////////////////////////////////////////
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Star")
        {
            if (!cargandoEstrella) sfx.PlayGame();//Sonido de recoger estrella, solo se reproduce si no se esta cargando una estrella.
            cargandoEstrella = true;//Indica que el jugador ahora esta cargando una estrella.
        }

        if (collision.tag == "Puerta")
        {
            contactoConPuerta = true;//Indica que el jugador esta en contacto con la puerta.
            //////////////////////////////////////////////////////////////////////////////////////////
            ///Si la puntuación del jugador es mayor o igual a 8, se activa el boton tactil Enter o
            ///el letrero de "Pulsa Enter Para Continuar".
            SetEnter = puntuacionPlayer >= puntosVictoria;
            botonEnter.SetActive(SetEnter);
            //////////////////////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////////////////////////
            //Si el jugador esta cargando una estrella, se suma un punto a la puntuación del jugador,
            //se reproduce el sonido de recoger estrella y se desactiva la variable que indica que el
            //jugador esta cargando una estrella.
            if (cargandoEstrella) { puntuacionPlayer++; sfx.PlayGame(); cargandoEstrella = false; }
            //////////////////////////////////////////////////////////////////////////////////////////
        }

        if (collision.tag == "Enemy")
        {
            //////////////////////////////////////////////////////////////////////////////////////////
            ///Estos codigos deciden si el jugador morira o perdera una estrella, verifican si el jugador
            ///tiene invulnerabilidad, godMode, esta cargando una estrella o si ya murio en el juego
            ///para evitar bugs.
            if (!invulnerabilityTime && !godMode && !cargandoEstrella && !alreadyDead) StartCoroutine(deadScreen(3f));
            if (!invulnerabilityTime && !godMode && cargandoEstrella) PerderEstrella();
            //////////////////////////////////////////////////////////////////////////////////////////
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Puerta"))
        {
            //////////////////////////////////////////////////////////////////////////////////////////
            ///Estos codigos desactivan el boton tactil Enter y/o el letrero de "Pulsa Enter Para Continuar".
            SetEnter = false;
            contactoConPuerta = false;
            botonEnter.SetActive(false);
            //////////////////////////////////////////////////////////////////////////////////////////
        }
    }
    private void PerderEstrella()
    {
        sfx.PlayDaño();//Sonido de perder una estrella.
        anim.SetBool("Perder_Estrella", true);//Animación de perder una estrella.
        invulnerabilityTime = true;//Se activa una invulnerabilidad para el jugador por 1 segundos.
        StartCoroutine(PerderEstrella(2f));//Corrutina que desactiva la invulnerabilidad del jugador y la animación de perder la estrella.
        cargandoEstrella = false;//Se desactiva la variable que indica que el jugador esta cargando una estrella.
        puntuacionParaDerrota++;//Se suma un punto a la variable que indica la puntuación para perder.

        //////////////////////////////////////////////////////////////////////////////////////////
        ///Si la puntuación para perder es igual a 3 se desactivan los botones de la pantalla
        ///táctil, se reproduce el sonido de derrota y se activa el menu de GameOver.
        if (puntuacionParaDerrota == 3)
        {
            btn_izq.enabled = btn_der.enabled = btn_salto.enabled = btnPause.enabled = false;
            sfx.PlayGG();
            menuGameOver.SetActive(true);
        }
        //////////////////////////////////////////////////////////////////////////////////////////
    }
    public int GetPuntos()
    {
        ///Devuelve la puntuación del jugador a la clase Anim De La Puerta
        ///y a la clase Puntaje.
        return puntuacionPlayer;
    }
    public bool GetCarryStar()
    {
        //Devuelve informacion a scripts externos sobre si el jugador esta cargando una estrella.
        return cargandoEstrella;
    }
    public bool GetEnter()
    {
        return SetEnter;//Devuelve el valor de SetEnter a la clase Enter.
    }
    public void BotonEnterTactil()
    {
        botonEnterCelular = true;
        StartCoroutine(DesactivarBotonTactil(.3f));
    }
    IEnumerator DesactivarBotonTactil(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        botonEnterCelular = false;
    }
    IEnumerator PerderEstrella(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        anim.SetBool("Perder_Estrella", false);
        yield return new WaitForSeconds(Tiempo + 1f);
        invulnerabilityTime = false;
    }
    IEnumerator Victory(float Tiempo)
    {
        sfx.PlayGame();
        yield return new WaitForSeconds(Tiempo);
        SceneManager.LoadScene("Victory");
    }
    IEnumerator deadScreen(float Tiempo)
    {
        mov.SetMuerte();//Se desactiva el movimiento del jugador.
        sfx.PlayDaño();//Sonido de daño.
        sfx.PlayGG();//Sonido de derrota.
        anim.SetBool("Muerte", true);//Animación de muerte.
        alreadyDead = true;//Indica que el jugador ya murio.
        btn_izq.enabled = btn_der.enabled = btn_salto.enabled = btnPause.enabled = false;//Desactiva los botones de la pantalla táctil.
        yield return new WaitForSeconds(Tiempo);
        menuGameOver.SetActive(true);//Activa el menu de GameOver.
    }
}