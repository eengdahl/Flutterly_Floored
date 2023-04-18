using HoudiniEngineUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum VitrinStates
{
    Idle,
    Patrol,
    Stop,
    Attack,
    Gaze,
    KillPlayer,
    Exit
}

public class VitrinBrain : MonoBehaviour
{


    [SerializeField] List<Transform> waypoints;
    public VitrinStates vitrinState;
    AudioManager audioManager;
    float speed;
    int index;
    public GameObject bloackade;

    AudioSource aS;
    public AudioClip crash;
    public AudioClip catSound;




    void Awake()
    {
        speed = 5f;
        audioManager = FindObjectOfType<AudioManager>();
        aS = GetComponent<AudioSource>();
        NextState();
    }

    private void FixedUpdate()
    {



        if (vitrinState == VitrinStates.Patrol)
        {

            if (Vector3.Distance(transform.position, waypoints[index].position) < 0.01f)
            {
                index++;
                this.gameObject.transform.Rotate(Vector3.up, 180);
            }
            if (index == waypoints.Count)
            {
                index = 0;
            }

            transform.position = Vector3.MoveTowards(
                  transform.position,
                  waypoints[index].position,
                  speed * Time.deltaTime);
        }
    }



    IEnumerator IdleState()
    {
        while (vitrinState == VitrinStates.Idle)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator PatrolState()
    {
        while (vitrinState == VitrinStates.Patrol)
        {
            int catTimerDone = Random.Range(1, 4);
            yield return new WaitForSeconds(catTimerDone);
            vitrinState = VitrinStates.Attack;

            yield return 0;
        }
        NextState();
    }

    IEnumerator StopState()
    {
        while (vitrinState == VitrinStates.Stop)
        {

            yield return new WaitForSeconds(1);

            NextState();
            yield return 0;
        }
    }

    IEnumerator AttackState()
    {
        while (vitrinState == VitrinStates.Attack)
        {
            aS.clip = catSound;
            aS.Play();
            yield return new WaitForSeconds(1);
            vitrinState = VitrinStates.Gaze;
            yield return 0;
        }
        NextState();
    }

    IEnumerator GazeState()
    {
        while (vitrinState == VitrinStates.Gaze)
        {
            yield return new WaitForSeconds(1);
            vitrinState = VitrinStates.Patrol;
            yield return 0;
        }
        NextState();
    }
    IEnumerator KillPlayerState()
    {
        while (vitrinState == VitrinStates.KillPlayer)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator ExitState()
    {
        while (vitrinState == VitrinStates.Exit)
        {
            Destroy(bloackade);

            aS.clip = crash;
            aS.Play();
            yield return new WaitForSeconds(2);
            audioManager.ResumeMainMusic();



            vitrinState = VitrinStates.Idle;
            yield return 0;
        }
        NextState();
    }


    void NextState()
    {
        string methodName = vitrinState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
