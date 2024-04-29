using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Estrella_Player : MonoBehaviour
{
    [SerializeField] private ConVic ConVic;
    [SerializeField] private SFX sfx;
    [SerializeField] private bool CarryStar;
    [SerializeField] private bool CarryStar_V = true;
    [SerializeField] private Light2D light2D;
    [SerializeField] private SpriteRenderer srEstrella;//estrella del jugador
    //[SerializeField] private GameObject goStar;//estrella del jugador
    //[SerializeField] private Image SR_Estrella_pantalla;//estrella en pantalla

    // Start is called before the first frame update
    void Start()
    {
        srEstrella = GetComponent<SpriteRenderer>();
        //goStar = gameObject;
        srEstrella.enabled = false;
        //goStar.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        //goStar.SetActive(true);
        srEstrella.enabled = true;
        light2D.enabled = true;
        //SR_Estrella_pantalla.enabled = true;
    }
    IEnumerator ApagarEstrella(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        //goStar.SetActive(false);
        srEstrella.enabled = false;
        light2D.enabled = false;
        //SR_Estrella_pantalla.enabled = false;
    }
}