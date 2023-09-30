using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BloquesEsp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TilemapRenderer tilemapRenderer;
    [SerializeField] private P_Secreto P_Secreto;
    private bool Secret = false;
    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Secret = P_Secreto.GetSecret();
        if (Secret)
        {
            //if (tilemapRenderer != null)
            //{
                tilemapRenderer.enabled = false; // Desactiva el TilemapRenderer
            //}
        }
    }
}
