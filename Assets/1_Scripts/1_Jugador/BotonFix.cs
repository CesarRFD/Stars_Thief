using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonFix : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Movement mov;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void JumpBtn()
    {
        Debug.Log("Fix");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //mov.BotonJump();
    }
}
