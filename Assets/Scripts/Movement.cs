using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float horizontal;
    [SerializeField] private float horizontalp;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 3;
    private float fuerzaSalto = 5f;
    private Rigidbody2D RB;
    private float fuerzaSaltoT = 8f;
    private float previousVelocity;
    private bool Jumping;
    private bool Bug = false;
    private bool BugT = false;
    private bool BugPI = false;
    private int tm = 2;//Variable para teclado o movil
    private bool isFalling;
    private bool anju = false;
    [SerializeField] private float torque = 1f;

    [SerializeField] private Joystick JS;

    [SerializeField] private SFX sfx;

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
        RB.velocity = new Vector2(horizontal, RB.velocity.y);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {
        if (Bug)
        {
            transform.Translate(Vector3.up * .15f);
        }
        if (BugT)
        {
            transform.Translate(Vector3.down * .15f);
        }
        if (BugPI)
        {
            transform.Translate(Vector3.right * .15f);
        }
        //codigo para desbugearse
        if (RB.velocity.y < 0)
        {
            isFalling = true;
            anim.SetBool("Cayendo", true);
            anju = false;
        }
        else
        {
            isFalling = false;
            anim.SetBool("Cayendo", false);
        }
        //Codigo animacion salto

        switch (tm)
        {
            case 1://teclado
                horizontal = Input.GetAxisRaw("Horizontal");
                js.SetActive(false);
                break;

            case 2://movil
                if (mp.getjs())
                {
                    horizontal = JS.Horizontal;
                    js.SetActive(true);
                }
                else
                {
                    horizontal = mp.gethorizontal();
                    js.SetActive(true);
                }
                break;
        }//Caminar
        anim.SetBool("Jumping", false);//Codigo animacion salto
        anim.SetBool("Run", horizontal != 0.0f);//codigo animacion correr

        //horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);//Caminar

        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
        }
        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
        if (anju)
        {
            anim.SetBool("Jumping", true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            anim.SetBool("Jumping", true);
            sfx.PlayJump();
        }///////////////////////////////////////////////////////////////////
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
        if (collision.tag == "Debug Square Pared Izq")
        {
            BugPI = true;
        }
        if (collision.tag == "Trigger Wira")
        {
            RB.constraints = RigidbodyConstraints2D.None;
            RB.AddTorque(torque);
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
    public void JumpBtn()
    {
        if (Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            anju = true;
            sfx.PlayJump();
            //anim.SetBool("Jumping", true);
            //Debug.Log("Salto Prueba");
        }
    }
}