using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    private SpriteRenderer EnterS;
    [SerializeField] private ConVic ConVic;
    void Start()
    {
        EnterS = GetComponent<SpriteRenderer>();
        EnterS.enabled = false;
    }
    void Update()
    {
        EnterS.enabled = ConVic.GetEnter();
    }
}