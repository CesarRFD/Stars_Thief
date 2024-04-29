using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSave : MonoBehaviour
{
    public int dificultad;
    [SerializeField] private GameObject[] enemigos = new GameObject[3];
    [SerializeField] private MenuPausa mp;

    void Start()
    {
        LoadData();
        switch (dificultad)
        {
            case 1:
                enemigos[0].SetActive(true);
                enemigos[1].SetActive(false);
                enemigos[2].SetActive(false);
                return;

            case 2:
                enemigos[0].SetActive(true);
                enemigos[1].SetActive(true);
                enemigos[2].SetActive(false);
                return;

            case 3:
                enemigos[0].SetActive(true);
                enemigos[1].SetActive(true);
                enemigos[2].SetActive(true);
                return;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Easy()
    {
        dificultad = 1;
        SaveData();
        mp.Retry();
    }
    public void Normal()
    {
        dificultad = 2;
        SaveData();
        mp.Retry();
    }
    public void Hard()
    {
        dificultad = 3;
        SaveData();
        mp.Retry();
    }
    private void SaveData()
    {
        SaveManager.SaveDificultData(this);
        //Debug.Log("Datos Guardados");
    }
    private void LoadData()
    {
        PlayerData dificultData = SaveManager.LoadDificultData();
        dificultad = dificultData.dificultad;
        //Debug.Log("Datos Cargados");
    }
}
