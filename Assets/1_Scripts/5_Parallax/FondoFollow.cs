using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoFollow : MonoBehaviour
{
    [SerializeField] private Transform player;


    void Update()
    {
        Vector3 seguirplayer = new Vector3(player.position.x, player.position.y, player.position.z);

        transform.position = seguirplayer;
    }
}
