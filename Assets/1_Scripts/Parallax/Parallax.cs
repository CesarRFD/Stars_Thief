using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenghtx, startposx;
    private float lenghty, startposy;
    public GameObject cam;
    public float parallaxEffectX;
    public float parallaxEffectY;

    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        lenghtx = GetComponent<SpriteRenderer>().bounds.size.x;
        lenghty = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        //Debug.Log("FixUpdate");
    }
    private void Update()
    {
        //Debug.Log("Update");
        float tempx = (cam.transform.position.x * (1 - parallaxEffectX));
        float distx = (cam.transform.position.x * parallaxEffectX);

        transform.position = new Vector3(startposx + distx, transform.position.y, transform.position.z);

        if (tempx > startposx + lenghtx) startposx += lenghtx;
        else if (tempx < startposx - lenghtx) startposx -= lenghtx;
        //////////////////////////////////////////////////////////////////
        float tempy = (cam.transform.position.y * (1 - parallaxEffectY));
        float disty = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(transform.position.x, startposy + disty, transform.position.z);

        if (tempy > startposy + lenghty) startposy += lenghty;
        else if (tempy < startposy - lenghty) startposy -= lenghty;
    }
}
