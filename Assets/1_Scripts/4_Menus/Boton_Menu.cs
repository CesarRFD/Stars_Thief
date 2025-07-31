using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton_Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }
    public void Menu(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
