/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        PlayerData playerData = SaveManager.LoadPlayerData();
        transform.position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);
        Debug.Log("Datos Cargados");
    }

    private void SaveData()
    {
        SaveManager.SavePLayerData(this);
        Debug.Log("Datos Guardados");
    }
}*/
