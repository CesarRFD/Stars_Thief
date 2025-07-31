using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    [SerializeField] private float velMovimiento;
    [SerializeField] private Transform player;

    private float initialZ;

    void Start()
    {
        initialZ = transform.position.z;
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.position, velMovimiento * Time.deltaTime);
        float targetZ = transform.position.z; // Mantener la posición Z actual
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, targetZ);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, velMovimiento * Time.deltaTime);
        transform.position = targetPosition;
    }
}
