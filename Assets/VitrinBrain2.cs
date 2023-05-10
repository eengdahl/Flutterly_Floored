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
    private Transform wakePoint;
    GazeOfDeath gaze;


    private float speed;
    private bool toSpot;
    private bool fromSpot;
    private bool locker;
    public bool catIsDead;


    private bool appearDone;

    private Vector3 orginalPos;

    private SwitchControls switchControls;
    LightSway mainLight;

    AudioManager audioManager;
    private AudioSource aS;
    public AudioClip catSound;


    //Animations
    private Animator animator;
    private string wakeAni = "rig_001_Vitrinskåp tassar upp";
    private string sweepAni = "rig_001_Vitrinskåp full sweep";
    private string deadAni = "rig_001_Vitrinskåp defeated";
    private string idleAni = "rig_001_Vitrinskåp mid idle";



    void Awake()
    {
        gaze = GetComponent<GazeOfDeath>();
        animator = GetComponent<Animator>();
        switchControls = FindAnyObjectByType<SwitchControls>();
        mainLight = FindAnyObjectByType<LightSway>();
        aS = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
     
       

        wakePoint = this.transform;
        catIsDead = false;
        locker = false;
        vitrinCat = this.gameObject;
        orginalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        animator.CrossFade(idleAni, 0, 0);
        activeState = VitrinState.Idle;
        NextState();
    }


    //private void FixedUpdate()
    //{
    //    if (catIsDead)
    //    {
    //        activeState = VitrinState.Exit;
    //    }
    //    if (activeState == VitrinState.Wake)
    //    {
    //        //if (toSpot)
    //        //{
    //        //    vitrinCat.transform.position = Vector3.MoveTowards(vitrinCat.transform.position, wakePoint.transform.position, speed * Time.deltaTime);
    //        //    if (Vector3.Distance(transform.position, wakePoint.transform.position) < 0.01f)
    //        //    {
    //        //        toSpot = false;
    //        //    }
    //        //}
    //        if (fromSpot)
    //        {
    //            vitrinCat.transform.position = Vector3.MoveTowards(vitrinCat.transform.position, orginalPos, speed * Time.deltaTime);
    //            if ((Vector3.Distance(transform.position, orginalPos) < 0.01f))
    //            {
    //                switchControls.SwitchToFloor();
    //                activeState = VitrinState.Appear;
    //            }
    //        }
    //    }
    //    if (activeState == VitrinState.Gaze)
    //    {
    //        float rY = Mathf.SmoothStep(-140, RotAngleY, Mathf.PingPong(Time.time * speed / 10, 1));
    //        vitrinCat.transform.rotation = Quaternion.Euler(0, rY, 0);
    //    }

    //}


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
                audioManager.TurnOfMainMusic();
                animator.CrossFade(wakeAni, 0, 0);
                aS.PlayOneShot(catSound);
                toSpot = true;
                locker = true;
            }
            if (appearDone)
            {
                switchControls.SwitchToFloor();
                activeState = VitrinState.Gaze;
            }


            yield return 0;
        }
        NextState();
    }
    IEnumerator AppearState()
    {
        while (activeState == VitrinState.Appear)
        {
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
            animator.CrossFade(sweepAni, 0, 0);

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

            gaze.locker = true;
            aS.PlayOneShot(catSound);
            animator.CrossFade(deadAni, 0, 0);
            yield return new WaitForSeconds(catSound.length);
            mainLight.ReSetLightInRoom();
            audioManager.ResumeMainMusic();
            this.gameObject.SetActive(false);
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


    void WakeComplete()
    {
        appearDone = true;
    }
}
