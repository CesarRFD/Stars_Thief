using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Tra : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("Jump", true);
            StartCoroutine(Esperar(1f));
        }
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("Jump", false);
        }
    }*/
    IEnumerator Esperar(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        anim.SetBool("Jump", false);
    }
}
