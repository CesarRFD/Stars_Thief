using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boton_enter : MonoBehaviour
{
    public GameObject boton;
    private bool btn = false;
    // Start is called before the first frame update
    private void Update()
    {
        if (btn)
        {
            boton.SetActive(true);
            Debug.Log("seta "+boton);
        }
        else
        {
            boton.SetActive(false);
            Debug.Log("seta " + boton);
        }
    }
    public void setBotonT()
    {
        btn = true;
        Debug.Log("setat " + boton);
    }
    public void setBotonF()
    {
        btn = false;
        Debug.Log("setaf " + boton);
    }
}
