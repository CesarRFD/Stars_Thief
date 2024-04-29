using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class MovementInd : MonoBehaviour
{
    //animaciones
    private Animator anim;
    private Rigidbody2D RB;
    private bool animJump = false;
    //private bool Jumping;
    //private bool isFalling;



    private float Horizontal;
    [SerializeField] private float speed = 3;
    private float fuerzaSalto = 5f;
    private float fuerzaSaltoT = 8f;
    private float previousVelocity;
    private bool Bug = false;
    private bool BugT = false;
    [SerializeField] private int seleccionControl = 2;//Variable para teclado o movil

    [SerializeField] private SFX sfx;
    //[SerializeField] private Joystick JS;
    //[SerializeField] private GameObject js;
    [SerializeField] private MenuPausa menuPausa;

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
        /////////////////
        //debug squares
        if (Bug)
        {
            transform.Translate(Vector3.up * .1f);
        }
        if (BugT)
        {
            transform.Translate(Vector3.down * .1f);
        }
        ////////////////

        ////////////////////////////////////////////////////////////////////////////
        if (RB.velocity.y < 0)//////////////////////////////////////////////////////
        {
            //isFalling = true;
            anim.SetBool("Cayendo", true);
            animJump = false;
            anim.SetBool("RJump", false);
        }
        else//////////////////////////Animacion de salto
        {
            //isFalling = false;
            anim.SetBool("Cayendo", false);
        }
        anim.SetBool("Jumping", false);///////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        

        //////////////////////////////////////////////////////////////
        switch (seleccionControl)
        {
            case 1://teclado
                Horizontal = Input.GetAxisRaw("Horizontal");
                //js.SetActive(false);
                break;

            case 2://movil
                Horizontal = menuPausa.Gethorizontal();
                //js.SetActive(true);
                break;
        }
        //////////////////////////////////////////////////////////////
        


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
        if (animJump)
        {
            anim.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            sfx.PlayJump();
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
    public void JumpBtn()
    {
        if (Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            animJump = true;
            sfx.PlayJump();
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
    }
}
