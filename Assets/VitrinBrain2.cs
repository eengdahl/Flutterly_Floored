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
    public GameObject gazePoint;

    private float speed;
    private bool toSpot;
    private bool fromSpot;
    private bool locker;
    public bool catIsDead;
    float RotAngleY = -60;

    private Vector3 orginalPos;

    private SwitchControls switchControls;

    AudioManager audioManager;
    private AudioSource aS;
    public AudioClip catStomp;
    public AudioClip crash;
    public AudioClip catSound;
    public AudioClip catSound0;

    void Awake()
    {
        catIsDead = false;
        locker = false;
        switchControls = FindAnyObjectByType<SwitchControls>();
        speed = 4f;
        vitrinCat = this.gameObject;
        aS = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
        NextState();
        orginalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }


    private void FixedUpdate()
    {
        if (activeState == VitrinState.Wake)
        {
            if (toSpot)
            {
                vitrinCat.transform.position = Vector3.MoveTowards(vitrinCat.transform.position, wakePoint.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, wakePoint.transform.position) < 0.01f)
                {
                    toSpot = false;
                }
            }
            if (fromSpot)
            {
                vitrinCat.transform.position = Vector3.MoveTowards(vitrinCat.transform.position, orginalPos, speed * Time.deltaTime);
                if ((Vector3.Distance(transform.position, orginalPos) < 0.01f))
                {
                    switchControls.SwitchToFloor();
                    activeState = VitrinState.Appear;
                }
            }
        }
        if (activeState == VitrinState.Gaze)
        {
            float rY = Mathf.SmoothStep(-120, RotAngleY, Mathf.PingPong(Time.time * speed / 10, 1));
            vitrinCat.transform.rotation = Quaternion.Euler(0, rY, 0);
        }

        if (catIsDead)
        {
            activeState = VitrinState.Exit;
        }
    }


    IEnumerator IdleState()
    {
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
            if (!locker)
            {
                toSpot = true;
                vitrinCat.GetComponent<MeshRenderer>().enabled = true;
                aS.PlayOneShot(catStomp);
                yield return new WaitForSeconds(catStomp.length);
                aS.PlayOneShot(catSound0);
                locker = true;
            }
            yield return new WaitForSeconds(catSound0.length);
            // toSpot = false;
            fromSpot = true;

            yield return 0;
        }
        NextState();
    }
    IEnumerator AppearState()
    {
        while (activeState == VitrinState.Appear)
        {
            vitrinCat.transform.position = gazePoint.transform.position;
            activeState = VitrinState.Gaze;
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
            Destroy(this.gameObject);
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
