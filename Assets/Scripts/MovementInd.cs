using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class MovementInd : MonoBehaviour
{
    private float Horizontal;
    private Animator anim;
    private float speed = 3;
    private float fuerzaSalto = 5f;
    private Rigidbody2D RB;
    private float fuerzaSaltoT = 8f;
    private float previousVelocity;
    private bool Jumping;
    private bool isFalling;
    private bool Bug = false;
    private bool BugT = false;
    private bool esp = false;
    private bool btn = false;
    private bool anju = false;
    [SerializeField] private int tm = 2;//Variable para teclado o movil

    [SerializeField] private SFX sfx;
    [SerializeField] private Joystick JS;
    [SerializeField] private GameObject js;
    [SerializeField] private MenuPausa mp;
    [SerializeField] private SpriteRenderer enters;//SP del cartelito de continuar
    [SerializeField] private SpriteRenderer cargando;//SP del cartelito de continuar
    [SerializeField] private GameObject enter;//GO del boton enter

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        previousVelocity = RB.velocity.y;
        anim = GetComponent<Animator>();
        StartCoroutine(Esperar(5));
    }
    private void FixedUpdate()
    {
        RB.velocity = new Vector2(Horizontal, RB.velocity.y);
    }
    void Update()
    {
        if (Bug)
        {
            transform.Translate(Vector3.up * .1f);
        }
        if (BugT)
        {
            transform.Translate(Vector3.down * .1f);
        }
        if (RB.velocity.y < 0)
        {
            isFalling = true;
            anim.SetBool("Cayendo", true);
            anju = false;
            anim.SetBool("RJump", false);
        }
        else
        {
            isFalling = false;
            anim.SetBool("Cayendo", false);
        }
        anim.SetBool("Jumping", false);
        //Horizontal = Input.GetAxisRaw("Horizontal");
        switch (tm)
        {
            case 1://teclado
                Horizontal = Input.GetAxisRaw("Horizontal");
                js.SetActive(false);
                break;

            case 2://movil
                if (mp.getjs())
                {
                    Horizontal = JS.Horizontal;
                    js.SetActive(true);
                }
                else
                {
                    Horizontal = mp.gethorizontal();
                    js.SetActive(true);
                }
                break;
        }
        anim.SetBool("Run", Horizontal != 0.0f);

        transform.Translate(Horizontal * speed * Time.deltaTime, 0, 0);//Caminar

        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-5.0f, 5.0f, 5.0f);
        }
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        }
        if (anju)
        {
            anim.SetBool("Jumping", true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            //anim.SetBool("Jumping", true);
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            sfx.PlayJump();
        }
        if ((btn || Input.GetKeyDown(KeyCode.Return)) && esp==true)
        {
            StartCoroutine(EsperarGG(1));
            //sfx.PlayGG();
            //SceneManager.LoadScene("Nivel_1");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trampolin")
        {
            RB.AddForce(new Vector2(0f, fuerzaSaltoT), ForceMode2D.Impulse);
            anim.SetBool("RJump", true);
            sfx.PlayJump();
        }
        if (collision.tag == "Debug Square")
        {
            Bug = true;
        }
        if (collision.tag == "Debug Square Invertido")
        {
            BugT = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Debug Square")
        {
            Bug = false;
        }
        if (collision.tag == "Debug Square Invertido")
        {
            BugT = false;
        }
    }
    IEnumerator Esperar(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        esp = true;
    }
    IEnumerator EsperarGG(float Tiempo)
    {
        enters.enabled = false;//desactiva el "Pulsa para continuar"
        cargando.enabled = true;//activa el "Cargando"
        enter.SetActive(false);//desactiva el boton "enter"
        sfx.PlayGame();
        yield return new WaitForSeconds(Tiempo);
        SceneManager.LoadScene("Nivel_1");//carga escena 1
    }
    public void JumpBtn()
    {
        if (Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            anju = true;
            sfx.PlayJump();
            //anim.SetBool("Jumping", true);
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
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
