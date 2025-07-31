using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private AudioSource dreamon;
    private AudioSource invadercoon;
    private AudioSource differentspace;
    private AudioSource scapeinspace;
    private AudioSource daño;
    private AudioSource gg;
    private AudioSource playgame;
    private AudioSource jump;
    [SerializeField] private AudioSource walk;
    void Start()
    {
        dreamon = GameObject.Find("Dream On").GetComponent<AudioSource>();
        invadercoon = GameObject.Find("Invader Coon").GetComponent<AudioSource>();
        differentspace = GameObject.Find("Different Space").GetComponent<AudioSource>();
        scapeinspace = GameObject.Find("Scape In Space").GetComponent<AudioSource>();
        daño = GameObject.Find("Daño").GetComponent<AudioSource>();
        gg = GameObject.Find("GG").GetComponent<AudioSource>();
        playgame = GameObject.Find("Play Game").GetComponent<AudioSource>();
        jump = GameObject.Find("Jump").GetComponent<AudioSource>();
        walk = GameObject.Find("Walk").GetComponent<AudioSource>();
    }
    public void PlayDreamOn()
    {
        dreamon.Play();
    }
    public void StopDreamOn()
    {
        dreamon.Stop();
    }
    public void PlayInvaderCoon()
    {
        invadercoon.Play();
    }
    public void StopInvaderCoon()
    {
        invadercoon.Stop();
    }
    public void PlayDifferentSpace()
    {
        differentspace.Play();
    }
    public void StopDifferentSpace()
    {
        differentspace.Stop();
    }
    public void PlayScapeInSpace()
    {
        scapeinspace.Play();
    }
    public void StopScapeInSpace()
    {
        scapeinspace.Stop();
    }
    public void PlayDaño()
    {
        //Debug.Log("Daño");
        daño.Play();
    }
    public void PlayGG()
    {
        //Debug.Log("GG");
        gg.Play();
    }
    public void PlayGame()
    {
        //Debug.Log("Play Game");
        playgame.Play();
    }
    public void PlayJump()
    {
        //Debug.Log("Jump");
        jump.Play();
    }
    public void MuteInvaderCoon()
    {
        invadercoon.Pause();
    }
    public void UnmuteInvaderCoon()
    {
        invadercoon.UnPause();
    }
    public void PlayWalk()
    {
        walk.Play();
    }
    public void StopWalk()
    {
        walk.Stop();
    }
}
