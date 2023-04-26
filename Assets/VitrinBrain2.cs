using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum VitrinState
{
    Idle,
    Wake,
    Appear,
    Gaze,
    KillPlayer,
    Exit
}
public class VitrinBrain2 : MonoBehaviour
{
    public VitrinState activeState;
    private GameObject vitrinCat;
    public GameObject wakePoint;

    private float speed;

    AudioManager audioManager;
    private AudioSource aS;
    public AudioClip catStomp;
    public AudioClip crash;
    public AudioClip catSound;
    public AudioClip catSound0;

    void Awake()
    {
        speed = 4f;
        vitrinCat = this.gameObject;
        aS = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
        NextState();
    }


    private void FixedUpdate()
    {
        if (activeState == VitrinState.Appear)
        {
            vitrinCat.transform.position = Vector3.MoveTowards(vitrinCat.transform.position, wakePoint.transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, wakePoint.transform.position) < 0.01f)//+Lite tid för Awwwkänlsa
            {
                activeState = VitrinState.Gaze;
            }
        }
    }


    IEnumerator IdleState()
    {
        Debug.Log("IdleState");
        while (activeState == VitrinState.Idle)
        {
            yield return 0;
        }
        NextState();
    }
    IEnumerator WakeState()
    {
        while (activeState == VitrinState.Wake)
        {
            vitrinCat.GetComponent<MeshRenderer>().enabled = true;
            aS.PlayOneShot(catStomp);
            yield return new WaitForSeconds(catStomp.length);
            aS.PlayOneShot(catSound0);
            yield return new WaitForSeconds(catSound0.length);
            activeState = VitrinState.Appear;
            yield return 0;
        }
        NextState();
    }
    IEnumerator AppearState()
    {
        Debug.Log("Appear");
        while (activeState == VitrinState.Appear)
        {
            //first gaze med fågel bakom kameran i fixed
            //lås upp kameran och byt till game 

            yield return 0;
        }
        NextState();
    }
    IEnumerator GazeState()
    {
        while (activeState == VitrinState.Gaze)
        {
            //let the gazeswap begin
            //track player? // chase player? 
            yield return 0;
        }
        NextState();
    }

    IEnumerator KillPlayerState()
    {
        while (activeState == VitrinState.KillPlayer)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator ExitState()
    {
        while (activeState == VitrinState.Exit)
        {
            aS.PlayOneShot(catSound);
            yield return new WaitForSeconds(catSound.length);
            //POFF cat disaperade 
            yield return 0;
        }
        NextState();
    }

    void NextState()
    {
        string methodName = activeState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
