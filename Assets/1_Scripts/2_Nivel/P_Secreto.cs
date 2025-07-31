using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Secreto : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Secreto = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Colisiona con el jugador
        {
            Secreto = true;
        }
    }
    public bool GetSecret()
    {
        return Secreto;
    }
}
