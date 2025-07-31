using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje_StarRush : MonoBehaviour
{
    private int puntos = 0;
    private TextMeshProUGUI textMesh;

    private Animator anim;
    [SerializeField] private ConVic ConVic;
    [SerializeField] private StarRush StarRush;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {

        puntos = StarRush.GetPuntos();
        //Debug.Log("Si");
        //Debug.Log(puntos);
        textMesh.text = puntos.ToString("0");
    }

    /*public void SumarPuntos(float puntosEntrada)
    {
        puntos = puntosEntrada;
    }*/
}
