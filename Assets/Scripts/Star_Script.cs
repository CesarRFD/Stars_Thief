using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Script : MonoBehaviour
{

    //[SerializeField] private float cantidadPuntos;
    //[SerializeField] private Puntaje puntaje;
    [SerializeField] private ConVic ConVic;
    [SerializeField] private bool CarryStarr = false;

    private Rigidbody2D Rigidbody2D; //acceder a las físicas
    private Animator Animator; //acceder a las animaciones
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CarryStarr = ConVic.GetCarryStar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && CarryStarr == false)
        {
            Animator.SetBool("Recogida",true);
            Destroy(gameObject, 1.5f);
            //puntaje.SumarPuntos(cantidadPuntos);
        }
    }
}
