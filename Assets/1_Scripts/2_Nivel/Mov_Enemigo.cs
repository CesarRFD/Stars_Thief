using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Enemigo : MonoBehaviour
{
    [SerializeField] private float velMovimiento = 1;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private ConVic conVic;

    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        conVic = FindObjectOfType<ConVic>();
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        velMovimiento = conVic.FreezeEnemys();
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
            Girar();
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
