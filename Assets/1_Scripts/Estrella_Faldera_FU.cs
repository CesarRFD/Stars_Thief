using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrella_Faldera_FU : MonoBehaviour
{
    [SerializeField] private Transform playerstar;
    [SerializeField] private float velocidad = 5f;
    private void FixedUpdate()
    {

        Vector3 seguirplayer = Vector3.Lerp(transform.position, playerstar.position, velocidad * Time.deltaTime);

        transform.position = seguirplayer;
    }
}
