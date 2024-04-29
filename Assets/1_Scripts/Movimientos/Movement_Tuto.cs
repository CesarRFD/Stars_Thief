using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Tuto : MonoBehaviour
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
    private int tm = 2;//Variable para teclado o movil

    [SerializeField] private Joystick JS;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject js;

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

        if (RB.velocity.y < 0)
        {
            //isFalling = true;
            anim.SetBool("Cayendo", true);
            anim.SetBool("RJump", false);
        }
        else
        {
            //isFalling = false;
            anim.SetBool("Cayendo", false);
        }
        //Horizontal = Input.GetAxisRaw("Horizontal");
        //Horizontal = JS.Horizontal;
        switch (tm)
        {
            case 1://teclado
                Horizontal = Input.GetAxisRaw("Horizontal");
                js.SetActive(false);
                break;

            case 2://movil
                Horizontal = JS.Horizontal;
                js.SetActive(true);
                break;
        }
        anim.SetBool("Jumping", false);
        anim.SetBool("Run", Horizontal != 0.0f);
        //Horizontal = Input.GetAxisRaw("Horizontal");//////////////////////
        transform.Translate(Horizontal * speed * Time.deltaTime, 0, 0);//Caminar

        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
        }
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(RB.velocity.y) < 0.001f)
        {
            anim.SetBool("Jumping", true);
            RB.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trampolin")
        {
            RB.AddForce(new Vector2(0f, fuerzaSaltoT), ForceMode2D.Impulse);
            anim.SetBool("RJump", true);
        }
    }
}