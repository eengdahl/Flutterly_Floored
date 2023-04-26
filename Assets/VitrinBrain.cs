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
    public bool isGazing;
    public bool catIsDead;
    public bool grace;

    [SerializeField] ScreenShake screenShake;
    public GameObject player;




    void Awake()
    {
        catIsDead = false;
        grace = false;
        speed = 5f;
        audioManager = FindObjectOfType<AudioManager>();
        aS = GetComponent<AudioSource>();
        NextState();
    }


    private void FixedUpdate()
    {


        if (transform.rotation == waypoints[1].rotation)
        {
            transform.rotation = Quaternion.Lerp(waypoints[1].rotation, waypoints[0].rotation, 5 * Time.deltaTime);
        }
        if (transform.rotation == waypoints[0].rotation)
        {
            transform.rotation = Quaternion.Lerp(waypoints[0].rotation, waypoints[1].rotation, 5 * Time.deltaTime);
        }
        if (catIsDead)
        {
            vitrinState = VitrinStates.Exit;
        }


        if (vitrinState == VitrinStates.Patrol)
        {

            //if (Vector3.Distance(transform.position, waypoints[index].position) < 0.01f)
            //{
            //    index++;
            //    this.gameObject.transform.Rotate(Vector3.up, 180);
            //}
            //if (index == waypoints.Count)
            //{
            //    index = 0;
            //}

            //   transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, speed * Time.deltaTime);
            // transform.rotation = Vector3.RotateTowards(transform.forward, (waypoints[index].position-transform.position));
            //  transform.LookAt(waypoints[index]);
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
            if (catIsDead)
            {
                vitrinState = VitrinStates.Exit;
            }

            int catTimerDone = Random.Range(1, 4);
            yield return new WaitForSeconds(catTimerDone);
            vitrinState = VitrinStates.Attack;

            yield return 0;
        }
        NextState();
    }
    //Might not be neccesary 
    IEnumerator StopState()
    {
        while (vitrinState == VitrinStates.Stop)
        {
            if (catIsDead)
            {
                vitrinState = VitrinStates.Exit;
            }
            yield return new WaitForSeconds(1);

            NextState();
            yield return 0;
        }
    }

    IEnumerator AttackState()
    {
        while (vitrinState == VitrinStates.Attack)
        {
            if (catIsDead)
            {
                vitrinState = VitrinStates.Exit;
            }
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
            if (catIsDead)
            {
                vitrinState = VitrinStates.Exit;
            }
            Vector3 tester = transform.position;
            Vector3 saver = tester;
            tester.y = tester.y - 3;
            this.gameObject.transform.position = tester;
            this.gameObject.transform.LookAt(player.transform);

            yield return new WaitForSeconds(2);


            if (!grace)
            {
                isGazing = true;
            }
            yield return new WaitForSeconds(2);
            grace = false;
            this.gameObject.transform.position = saver;
            this.gameObject.transform.LookAt(waypoints[index].transform);
            isGazing = false;
            vitrinState = VitrinStates.Patrol;
            yield return 0;
        }
        NextState();
    }
    IEnumerator KillPlayerState()
    {
        while (vitrinState == VitrinStates.KillPlayer)
        {
            if (catIsDead)
            {
                vitrinState = VitrinStates.Exit;
            }

            yield return 0;
        }
        NextState();
    }

    IEnumerator ExitState()
    {
        while (vitrinState == VitrinStates.Exit)
        {
            catIsDead = true;

            Debug.Log("enterExit");
            Destroy(bloackade);

            aS.clip = crash;
            aS.loop = false;


            aS.Play();
            yield return new WaitForSeconds(2);
            aS.Stop();
            audioManager.ResumeMainMusic();
            this.gameObject.SetActive(false);

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

    public void ShakeMovement()
    {
        int randomTimer = Random.Range(1, 3);
        Invoke("ShakeMovement", randomTimer);

        screenShake.shakeCatMoves();
    }
}
