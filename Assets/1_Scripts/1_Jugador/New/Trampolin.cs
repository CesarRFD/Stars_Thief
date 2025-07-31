using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float fuerza = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
