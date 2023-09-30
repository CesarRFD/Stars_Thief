using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowStar : MonoBehaviour
{
    [SerializeField] private Transform playerstar;
    [SerializeField] private float velocidad = 5f;
    private void Update()
    {

        Vector3 seguirplayer = Vector3.Lerp(transform.position, playerstar.position, velocidad * Time.deltaTime);

        transform.position = seguirplayer;
    }
}
