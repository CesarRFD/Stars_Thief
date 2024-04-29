using UnityEngine;

public class SingWira : MonoBehaviour
{
    private AudioSource dreamon;
    private AudioSource invadercoon;
    private AudioSource differentspace;
    private AudioSource scapeinspace;
    private AudioSource daño;
    private AudioSource gg;
    private AudioSource playgame;
    private AudioSource jump;

    private void Start()
    {
        dreamon = GameObject.Find("Dream On").GetComponent<AudioSource>();
        invadercoon = GameObject.Find("Invader Coon").GetComponent<AudioSource>();
        differentspace = GameObject.Find("Different Space").GetComponent<AudioSource>();
        scapeinspace = GameObject.Find("Scape In Space").GetComponent<AudioSource>();
        daño = GameObject.Find("Daño").GetComponent<AudioSource>();
        gg = GameObject.Find("GG").GetComponent<AudioSource>();
        playgame = GameObject.Find("Play Game").GetComponent<AudioSource>();
        jump = GameObject.Find("Jump").GetComponent<AudioSource>();
        PlayInvaderCoon();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trigger Wira")
        {
            StopInvaderCoon();
            PlayDreamOn();
        }
    }
    private void PlayDreamOn()
    {
        dreamon.Play();
    }
    private void StopInvaderCoon()
    {
        invadercoon.Stop();
    }
    private void PlayInvaderCoon()
    {
        invadercoon.Play();
    }
}
