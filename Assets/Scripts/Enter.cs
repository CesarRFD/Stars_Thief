using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer EnterS;
    //[SerializeField] private bool CtoPuerta = false;
    [SerializeField] private ConVic ConVic;
    [SerializeField] private Player_Tutorial PT;
    [SerializeField] private boton_enter botonenter;
    //[SerializeField] private bool EnterX;
    //[SerializeField] private bool EnterY;
    // Start is called before the first frame update
    void Start()
    {
        EnterS = GetComponent<SpriteRenderer>();
        EnterS.enabled = false;
        botonenter.setBotonF();
    }
    void Update()
    {
        
        if (ConVic.GetEnter()/* || PT.GetEnter()*/)
        {
            //Debug.Log("Si");
            EnterS.enabled = true;
            botonenter.setBotonT();
        }
        else
        //if (ConVic.GetEnter() == false)
        {
            //Debug.Log("No");
            EnterS.enabled = false;
            botonenter.setBotonF();
        }
        /*if (PT.GetEnter() == false)
        {
            //Debug.Log("No");
            Debug.Log("Set false_2");
            EnterS.enabled = false;
        }*/
    }
    
}/*public void SetCtoPuerta(bool SetCtoPuerta)
    {
        if (SetCtoPuerta)
        {
            //Debug.Log("True");
            CtoPuerta = true;
        }
        else
        {
            //Debug.Log("False");
            CtoPuerta = false;
        }
    }*/
//EnterX = ConVic.GetEnter();
        //EnterY = PT.GetEnter();
        //Debug.Log("El EnterX es: " + EnterX);
        /*if (PT.GetEnterT())
        {
            //Debug.Log("Si");
            Debug.Log("Set true");
            EnterS.enabled = true;
        }
        else
        //if (ConVic.GetEnter() == false)
        {
            //Debug.Log("No");
            Debug.Log("Set false");
            EnterS.enabled = false;
        }*/