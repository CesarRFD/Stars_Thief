using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private bool esp;
    void Start()
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(Esperar(5));
    }
    IEnumerator Esperar(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        canvas.gameObject.SetActive(false);
    }
}
