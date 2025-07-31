using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Script : MonoBehaviour
{
    [SerializeField] private ConVic ConVic;
    private bool CarryStarr = false;
    private Animator Animator; //acceder a las animaciones

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

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
        }
    }
}
