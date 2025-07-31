using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wira : MonoBehaviour
{
    [SerializeField] FondoFollowY ff;
    [SerializeField] private Rigidbody2D rb;
    static private readonly float torque = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///Este Metodo Sirve Para Activar El ******.
        if (collision.tag == "Trigger Wira")
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(torque);
            ff.SingWira();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
}
