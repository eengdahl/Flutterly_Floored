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
    public List<Transform> walkPath;
    public Transform currentTarget;
    [SerializeField] private Light eyes;


    private void Start()
    {

        eyes = GetComponent<Light>();
        walkPath = new List<Transform>();
        var temp = GameObject.FindGameObjectsWithTag("WalkPath");
        NextState();

        foreach (var item in temp)
        {
            walkPath.Add(item.transform);
        }
    }


    IEnumerator IdleState()
    {
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
            yield return 0;
        }
        Debug.Log("Attack: Exit");
        NextState();
    }
    IEnumerator HitAttackState()
    {
        Debug.Log("HitAttack: Enter");
        while (activeVitrinState == VitrinState.HitAttack)
        {
            yield return 0;
        }
        Debug.Log("HitAttack: Exit");
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
