using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enter_Ind : MonoBehaviour
{
    private bool delay = false;
    private bool boton = false;

    //[SerializeField] private Image tagEnter;
    [SerializeField] private SFX sfx;
    [SerializeField] private GameObject botonEnter;//Boton Enter
    [SerializeField] private GameObject logoCargando;//Circulos de cargando
    [SerializeField] private Image textoEnter;//Image del cartelito de continuar
    [SerializeField] private Image textoCargando;//Image del cartelito de continuar

    void Start()
    {
        //tagEnter = GetComponent<Image>();
        botonEnter.SetActive(false);
        logoCargando.SetActive(true);
        textoEnter.enabled = false;
        textoCargando.enabled = false;
        StartCoroutine(Esperar(5));
    }
    private void Update()
    {
        if ((boton || Input.GetKeyDown(KeyCode.Return)) && delay == true)
        {
            StartCoroutine(BotonPresionado(1));
        }
    }
    IEnumerator Esperar(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        botonEnter.SetActive(true);
        logoCargando.SetActive(false);
        textoEnter.enabled = true;
        textoCargando.enabled = false;
        delay = true;
    }
    IEnumerator BotonPresionado(float Tiempo)
    {
        botonEnter.SetActive(false);
        logoCargando.SetActive(false);
        textoEnter.enabled = false;
        textoCargando.enabled = true;
        sfx.PlayGame();
        yield return new WaitForSeconds(Tiempo);
        SceneManager.LoadScene("Nivel_1");//carga escena 1
    }
    public void ActivarBoton()
    {
        boton = true;
        StartCoroutine(ApagarBoton(.3f));
    }
    IEnumerator ApagarBoton(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        boton = false;
    }
}
