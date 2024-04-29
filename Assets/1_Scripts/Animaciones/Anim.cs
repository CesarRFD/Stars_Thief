using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    //[SerializeField] private int puntos = 0;//Puntos del jugador
    private Animator anim;
    [SerializeField] private ConVic ConVic;
    [SerializeField] private StarRush StarRush;
    //private bool Hit = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ConVic.GetPuntos() == 8)
        {
            anim.SetBool("Abrir Puerta", true);
        }
    }
}