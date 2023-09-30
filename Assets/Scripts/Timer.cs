using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float elapsedTime = 0f;
    private bool gameEnd = false;
    public MenuPausa MenuPausa;

    [SerializeField] private GameObject menuGameOver;

    // Update is called once per frame
    void Update()
    {
        elapsedTime = elapsedTime - Time.deltaTime;

        if (gameEnd == false)
        {
            text.text = elapsedTime.ToString("F1");
        }
        if (elapsedTime <= 0 && gameEnd == false) // Con esta condicion indica que el tiempo acabo y que active el menu de GameOver
        {
            Debug.Log("El timer acabo");
            gameEnd = true;
            if (gameEnd == true)
            {
                menuGameOver.SetActive(true);
            }
        }
    }

    public void Reiniciar() //Esta funcion sirve para reiniciar la escena
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        MenuPausa.GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Volver(string nombre) //Con esta funcion te regresa a la escena que tu elijas
    {
        SceneManager.LoadScene(nombre);
    }
}