using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Esperar(25));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Esperar(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        SceneManager.LoadScene("Indicaciones");
    }
}
