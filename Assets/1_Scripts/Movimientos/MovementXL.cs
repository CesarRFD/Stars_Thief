using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementXL : MonoBehaviour
{
    private float Horizontal;
    private Animator anim;
    private float speed = 3;
    private float fuerzaSalto = 5f;
    private Rigidbody2D RB;
    private float fuerzaSaltoT = 8f;
    private float previousVelocity;
    private bool Jumping;
    //private bool isFalling;
    private bool Bug = false;
    private bool BugT = false;
    private bool anju = false;
    [SerializeField] private int tm = 2;//Variable para teclado o movil

    [SerializeField] private SFX sfx;
    [SerializeField] private Joystick JS;
    [SerializeField] private GameObject js;
    [SerializeField] private MenuPausa mp;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        previousVelocity = RB.velocity.y;
        anim = GetComponent<Animator>();
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
            //isFalling = true;
            anim.SetBool("Cayendo", true);
            anju = false;
            anim.SetBool("RJump", false);
        }
        else
        {
            //isFalling = false;
            anim.SetBool("Cayendo", false);
        }
        switch (tm)
        {
            case 1://teclado
                Horizontal = Input.GetAxisRaw("Horizontal");
                js.SetActive(false);
                break;

            case 2://movil
                Horizontal = mp.Gethorizontal();
                js.SetActive(true);
                break;
        }
        anim.SetBool("Jumping", false);
        //Horizontal = JS.Horizontal;
        //Horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetBool("Run", Horizontal != 0.0f);

        //Horizontal = Input.GetAxisRaw("Horizontal");//////////////////////
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
            anim.SetBool("Jumping", true);
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            sfx.PlayJump();
        }
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }*/
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
    public void ls()
    {
        StartCoroutine(EsperarGG(1));
        //SceneManager.LoadScene("MenuPrincipal");
    }
    IEnumerator EsperarGG(float Tiempo)
    {
        sfx.PlayGame();
        yield return new WaitForSeconds(Tiempo);
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void JumpBtn()
    {
        if (Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            sfx.PlayJump();
            anju = true;
            anim.SetBool("Jumping", true);
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
    }

}
