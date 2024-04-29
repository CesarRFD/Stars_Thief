using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Image = UnityEngine.UI.Image;

public class StarRush : MonoBehaviour
{
    [SerializeField] private int StarCount = 0;
    [SerializeField] private bool SetEnter;
    [SerializeField] private bool CtoPuerta = false;
    [SerializeField] private bool invulnerabilityTime = false;
    [SerializeField] private bool godMode = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] public bool CarryStar = false;//public
    private bool btn = false;//Unserialized

    [SerializeField] private float groundCheckRadius = 0.25f;

    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private GameObject btnenter;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Puntaje puntaje;
    [SerializeField] private Anim Anim;
    [SerializeField] private Enter Enter;
    [SerializeField] private SFX sfx;
    [SerializeField] private Image btnPause;
    [SerializeField] private Movement mov;
    private Animator anim;//Unserialized

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private UnityEngine.UI.Image btn_izq;
    [SerializeField] private UnityEngine.UI.Image btn_der;
    [SerializeField] private UnityEngine.UI.Image btn_salto;

    //private Rigidbody2D RB;


    private void Awake()
    {
        //RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        btnenter.SetActive(false);
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);//detecta si el jugador esta en el suelo

    if (StarCount == 3)
    {
        anim.SetBool("Abrir Puerta", true);  // Establece el parámetro de animación "Abrir Puerta" en verdadero
    }

    if ((btn || Input.GetKeyDown(KeyCode.Return)) && CtoPuerta && StarCount >= 5)//Condicion final para ganar
    {
        StartCoroutine(Victory(1.5f));  // Inicia la corrutina "Victory" después de cumplir ciertas condiciones
    }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Star")
        {
            StarCount++;
            sfx.PlayGame();
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Star")
        {
            sfx.PlayGame();
        }
        if (collision.tag == "Puerta")
        {
            CtoPuerta = true;
        }
        if (collision.tag == "Enemy")
        {
            if (/*cambiar por vidas* && */!isGrounded && !invulnerabilityTime && !godMode || !CarryStar && isGrounded && !invulnerabilityTime && !godMode)
            {
                StartCoroutine(ggScreen(.5f));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Puerta"))
        {
            SetEnterPuerta(false);
            CtoPuerta = false;
            btnenter.SetActive(false);
        }
    }
    private void SetEnterPuerta(bool SetCtoPuerta)
    {
        if (SetCtoPuerta)
        {
            SetEnter = true;
        }
        else
        {
            SetEnter = false;
        }
    }
    public int GetPuntos()
    {
        return StarCount;
    }
    public bool GetEnter()
    {
        return SetEnter;
    }
    public void FnBtn()
    {
        btn = true;
        StartCoroutine(EsperarBtn(.3f));
    }
    IEnumerator Victory(float Tiempo)
    {
        sfx.PlayGame();
        yield return new WaitForSeconds(Tiempo);
        SceneManager.LoadScene("Victory");
    }
    IEnumerator EsperarBtn(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        btn = false;
    }
    IEnumerator ggScreen(float Tiempo)
    {
        sfx.PlayDaño();
        anim.Play("muerte");
        btn_izq.enabled = false;
        btn_der.enabled = false;
        btn_salto.enabled = false;
        btnPause.enabled = false;
        yield return new WaitForSeconds(Tiempo);
        sfx.PlayGG();
        menuGameOver.SetActive(true);
    }
}