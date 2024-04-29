using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseMenuOptions;
    [SerializeField] private GameObject pauseMenuControls;
    [SerializeField] private GameObject pauseMenuDificulty;
    [SerializeField] private GameObject help;
    //[SerializeField] private GameObject js;
    //[SerializeField] private GameObject btnjoys;
    //[SerializeField] private GameObject options;

    [SerializeField] private GameObject movimiento;
    [SerializeField] private Image botonPausa;
    [SerializeField] private GameObject mute;
    [SerializeField] private GameObject unmute;
    [SerializeField] private SFX sfx;
    [SerializeField] private Movement mov;

    private bool horizontalL;
    private bool horizontalD;
    private float dir = 0;

    //Archivos de guardado
    public bool muteG = false;
    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        
    }
    
    void Update()
    {
        //Debug.Log(muteG);
        if (muteG)
        {
            MuteOn();
            sfx.MuteInvaderCoon();
        }
        else
        {
            MuteOff();
            sfx.UnmuteInvaderCoon();
        }
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
    /*public void Ejecutar()
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
    }*/
    /*public void OptionsT()
    {
        options.SetActive(true);
    }
    public void OptionsF()
    {
        options.SetActive(false);
    }*/
    public void InsT()
    {
        help.SetActive(true);
    }
    public void InsF()
    {
        help.SetActive(false);
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        botonPausa.enabled = true;
        movimiento.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        mov.SetPlayOn();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        botonPausa.enabled = false;
        movimiento.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        mov.SetPlayOff();
    }

    public void MenuPrincipal(string nombre)
    {
        sfx.PlayGame();
        //StartCoroutine(MenuSFX(3));
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
    IEnumerator MenuSFX(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        MenuPrincipalSFX();
    }
    private void MenuPrincipalSFX()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Retry()
    {
        sfx.PlayGame();
        //SceneManager.LoadScene("Nivel_1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        MenuPausa.GameIsPaused = false;
        Time.timeScale = 1f;
    }

        /*public void joysT()
        {
            joystick = true;
        }
        public void joysF()
        {
            joystick = false;
        }*/

        /*public bool getjs()
        {
            return joystick;
        }*/

        public void LeftTrue()
    {
        horizontalL = true;
    }
    public void LeftFalse()
    {
        horizontalL = false;
    }
    public void DerechaTrue()
    {
        horizontalD = true;
    }
    public void DerechaFalse()
    {
        horizontalD = false;
    }
    public float Gethorizontal()
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
    public void MuteOn()
    {
        muteG = true;
        mute.SetActive(false);
        unmute.SetActive(true);
        SaveData();

    }
    public void MuteOff()
    {
        muteG = false;
        mute.SetActive(true);
        unmute.SetActive(false);
        SaveData();
    }
    private void LoadData()
    {
        PlayerData configData = SaveManager.LoadConfigData();
        muteG = configData.mute;
        //Debug.Log("Datos Cargados");
    }

    public void SaveData()
    {
        SaveManager.SaveConfigData(this);
        //Debug.Log("Datos Guardados");
    }
    public void OptionsOn()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuOptions.SetActive(true);
    }
    public void OptionsOff()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuOptions.SetActive(false);
    }
    public void ControlsOn()
    {
        pauseMenuControls.SetActive(true);
        pauseMenuOptions.SetActive(false);
    }
    public void ControlsOff()
    {
        pauseMenuControls.SetActive(false);
        pauseMenuOptions.SetActive(true);
    }
    public void DificultyOn()
    {
        pauseMenuDificulty.SetActive(true);
        pauseMenuOptions.SetActive(false);
    }
    public void DificultyOff()
    {
        pauseMenuDificulty.SetActive(false);
        pauseMenuOptions.SetActive(true);
    }
}