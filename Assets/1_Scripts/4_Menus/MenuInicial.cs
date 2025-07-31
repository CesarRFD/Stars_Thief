using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void JugarT()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
