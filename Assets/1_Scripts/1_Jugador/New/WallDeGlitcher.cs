using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeGlitcher : MonoBehaviour
{
    [Space]
    [Header("Debug Squares.")]
    private bool Bug = false;
    private bool BugT = false;
    private bool BugPI = false;
    void Start()
    {
        
    }
    void Update()
    {
        #region Debug
        /////////////////////////////////////////////////////////////////////////////////////////////
        //Codigos para desbugearse.
        if (Bug) transform.Translate(Vector3.up * .15f);
        if (BugT) transform.Translate(Vector3.down * .15f);
        if (BugPI) transform.Translate(Vector3.right * .15f);
        /////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Metodos Para Desbuggearse De La Pared.
        if (collision.tag == "Debug Square") transform.Translate(Vector3.up * .15f);
        if (collision.tag == "Debug Square Invertido") transform.Translate(Vector3.down * .15f);
        if (collision.tag == "Debug Square Pared Izq") transform.Translate(Vector3.right * .15f);
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Estos Metodos Sirven Para Desactivar El Desbuggeador De La Pared.
        if (collision.tag == "Debug Square") Bug = false;
        if (collision.tag == "Debug Square Invertido") BugT = false;
        if (collision.tag == "Debug Square Pared Izq") BugPI = false;
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Este Metodo Sirve Para Desbuggearse De La Pared.
        if (collision.tag == "Debug Square") Bug = true;
        if (collision.tag == "Debug Square Invertido") BugT = true;
        if (collision.tag == "Debug Square Pared Izq") BugPI = true;
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///
    }
}
