using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Ind : MonoBehaviour
{
    [SerializeField] private SpriteRenderer EnterS;
    [SerializeField] private GameObject enter;
    // Start is called before the first frame update
    void Start()
    {
        EnterS = GetComponent<SpriteRenderer>();
        EnterS.enabled = false;
        StartCoroutine(Esperar(5));
    }
    IEnumerator Esperar(float Tiempo)
    {
        yield return new WaitForSeconds(Tiempo);
        EnterS.enabled = true;
        enter.SetActive(true);
    }
}
