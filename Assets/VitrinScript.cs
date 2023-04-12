using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
public enum VitrinState
{
    Idle,
    Patrol,
    Stop,
    Attack,
    HitAttack

}

public class VitrinScript : MonoBehaviour
{
    public VitrinState activeVitrinState;
    private List<Transform> walkPath;
    private Transform currentTarget;
    private Light eyes;


    private void Start()
    {

        eyes = GetComponent<Light>();
        walkPath = new List<Transform>();
        var tempPath = GameObject.FindGameObjectsWithTag("WalkPath");

        foreach (var item in tempPath)
        {
            walkPath.Add(item.transform);
        }
        NextState();
    }

    //Set activeVitrinState = VitrinState.X to change state. Use WaitForSeconds(x); for timers
    IEnumerator IdleState()
    {
        //Starts in this state, should maybe be asleep
        Debug.Log("Idle: Enter");
        while (activeVitrinState == VitrinState.Idle)
        {
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        NextState();
    }
    IEnumerator PatrolState()
    {
        Debug.Log("Patrol: Enter");
        while (activeVitrinState == VitrinState.Patrol)
        {
            //Choose random position to stop cat along the shelf
            yield return 0;
        }
        Debug.Log("Patrol: Exit");
        NextState();
    }
    IEnumerator StopState()
    {
        Debug.Log("Stop: Enter");
        while (activeVitrinState == VitrinState.Stop)
        {
            //Stay and cast shadow. Start timer before attack
            yield return 0;
        }
        Debug.Log("Stop: Exit");
        NextState();
    }
    IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        while (activeVitrinState == VitrinState.Attack)
        {
            //Hit this mother_f bird
            //Check if bird is hit, otherwise return to patrole
            yield return 0;
        }
        Debug.Log("Attack: Exit");
        NextState();
    }
    IEnumerator HitState()
    {
        Debug.Log("Hit: Enter");
        while (activeVitrinState == VitrinState.HitAttack)
        {
            //Kill bird
            yield return 0;
        }
        Debug.Log("Hit: Exit");
        NextState();
    }

    public void NextState()
    {
        //Stolen and works like a charm 
        string methodName = activeVitrinState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }



}
