using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject help;
    [SerializeField] private GameObject js;
    [SerializeField] private GameObject flechas;
    [SerializeField] private GameObject btnjoys;
    [SerializeField] private GameObject options;
    [SerializeField] private Image rawImage;
    [SerializeField] private Image btnsalto;

    [SerializeField] private Image btn_izq;//Desactivan los controles al pausar el juego
    [SerializeField] private Image btn_der;
    [SerializeField] private Image btn_pause;
    [SerializeField] private Image btn_pause2;

    private bool joystick = false;
    private bool horizontalL;
    private bool horizontalD;
    private float dir = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void ejecutar()
    {
        if (joystick)
        {
            btnjoys.SetActive(true);
            flechas.SetActive(false);
        }
        else
        {
            btnjoys.SetActive(false);
            flechas.SetActive(true);
        }
    }
    public void optionsT()
    {
        options.SetActive(true);
    }
    public void optionsF()
    {
        options.SetActive(false);
    }
    public void insT()
    {
        help.SetActive(true);
    }
    public void insF()
    {
        help.SetActive(false);
    }
    public void Resume()
    {
        btn_pause.enabled = true;
        btn_pause2.enabled = true;
        btn_izq.enabled = true;
        btn_der.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        rawImage.enabled = true;
        btnsalto.enabled = true;
        js.SetActive(true);
        //btnjoys.SetActive(true);
    }

    public void Pause()
    {
        btn_pause.enabled = false;
        btn_pause2.enabled = false;
        btn_izq.enabled = false;
        btn_der.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        rawImage.enabled = false;
        btnsalto.enabled = false;
        js.SetActive(false);
        btnjoys.SetActive(false);
        flechas.SetActive(false);

    }

    public void Menu(string nombre)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombre);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Nivel_1");
    }
    public void joysT()
    {
        joystick = true;
    }
    public void joysF()
    {
        joystick = false;
    }
    public bool getjs()
    {
        return joystick;
    }
    public void left()
    {
        horizontalL = true;
    }
    public void leftF()
    {
        horizontalL = false;
    }
    public void derecha()
    {
        horizontalD = true;
    }
    public void derechaF()
    {
        horizontalD = false;
    }
    public float gethorizontal()
    {
        if (horizontalL)
        {
            dir = -1f;
        }
        if (horizontalD)
        {
            dir = 1f;
        }
        if (horizontalL == false && horizontalD == false)
        {
            dir = 0f;
        }
        return dir;
    }
}
