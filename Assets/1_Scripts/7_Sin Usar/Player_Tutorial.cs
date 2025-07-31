using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class Player_Tutorial : MonoBehaviour
{
    private bool Estrella = false;
    private bool CarryStar = false;
    private bool CtoPuerta = false;
    [SerializeField] private bool SetEnter;
    [SerializeField] private SFX sfx;
    private bool btn = false;
    [SerializeField] private GameObject enter;
    [SerializeField] private SpriteRenderer EnterS;

    // Start is called before the first frame update
    void Start()
    {
        //sfx.StopInvaderCoon();
    }

    // Update is called once per frame
    public void fnbtn()
    {
        btn = true;
        StartCoroutine(EsperarBtn(.3f));
    }
    IEnumerator EsperarBtn(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        btn = false;
        //Debug.Log(btn);
    }
    void Update()
    {
        if (CtoPuerta && Estrella && /*Input.GetKeyDown(KeyCode.Return) ||*/ btn)
        {
            //Debug.Log("Si");
            sfx.PlayGame();
            SceneManager.LoadScene("Indicaciones");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Star")
        {
            CarryStar = true;
        }
        if (collision.tag == "Puerta" && CarryStar)
        {
            Estrella = true;
        }
        if(collision.tag == "Puerta" && Estrella)
        {
            CtoPuerta = true;
            SetEnterPuerta(true);
            enter.SetActive(true);
            EnterS.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Puerta"))
        {
            SetEnterPuerta(false);
            CtoPuerta = false;
            enter.SetActive(false);
            EnterS.enabled = false;
        }
    }
    public bool getEstrella()
    {
        return Estrella;
    }
    private void SetEnterPuerta(bool SetCtoPuerta)
    {
        if (SetCtoPuerta)
        {
            //Debug.Log("True");
            SetEnter = true;
        }
        else
        {
            //Debug.Log("False");
            SetEnter = false;
        }
    }
    public bool GetEnterT()
    {
        return SetEnter;
    }
}