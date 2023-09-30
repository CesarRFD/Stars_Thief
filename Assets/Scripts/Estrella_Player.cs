using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Estrella_Player : MonoBehaviour
{
    [SerializeField] private ConVic ConVic;
    [SerializeField] private SFX sfx;
    [SerializeField] private bool CarryStar;
    [SerializeField] private bool CarryStar_V = true;

    [SerializeField] private SpriteRenderer SR_Estrella;//estrella del jugador
    [SerializeField] private Image SR_Estrella_pantalla;//estrella en pantalla

    // Start is called before the first frame update
    void Start()
    {
        SR_Estrella = GetComponent<SpriteRenderer>();
        SR_Estrella.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CarryStar = ConVic.GetCarryStar();
        CarryStar = ConVic.GetCarryStar();
        if (CarryStar == false)
        {
            StartCoroutine(ApagarEstrella(.5f));
            CarryStar_V = true;
        }
        if (CarryStar && CarryStar_V)
        {
            StartCoroutine(EncenderEstrella(1.5f));
            CarryStar_V = false;
        }
    }
    IEnumerator EncenderEstrella(float Tiempo)
    {
        sfx.PlayGame();
        yield return new WaitForSeconds(Tiempo);
        SR_Estrella.enabled = true;
        SR_Estrella_pantalla.enabled = true;
    }
    IEnumerator ApagarEstrella(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        SR_Estrella.enabled = false;
        SR_Estrella_pantalla.enabled = false;
    }
}