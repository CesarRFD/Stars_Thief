using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_Tutorial : MonoBehaviour
{
    private Animator anim;
    private bool Estrella;
    [SerializeField] private Player_Tutorial PT;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Estrella = PT.getEstrella();
        if (Estrella)
        {
            anim.SetBool("Abrir Puerta", true);
        }
    }
}
