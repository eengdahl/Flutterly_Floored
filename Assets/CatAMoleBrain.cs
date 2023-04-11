using HoudiniEngineUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatAMoleBrain : MonoBehaviour
{
    public enum MoleStates
    {
        Idle,
        Charge,
        Attack,
        HitBird,
        WaterHit
    }


    MoleStates moleStates;

    IEnumerator IdleState()
    {
        Debug.Log("Idle: Enter");
        while (moleStates == MoleStates.Idle)
        {
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        NextState();
    }

    IEnumerator ChargeState()
    {
        Debug.Log("Charge: Enter");
        while (moleStates == MoleStates.Charge)
        {
            yield return 0;
        }
        Debug.Log("Charge: Exit");
        NextState();
    }


    IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        while (moleStates == MoleStates.Attack)
        {
            yield return 0;
        }
        Debug.Log("Attack: Exit");
    }
    IEnumerator HitBirdState()
    {
        Debug.Log("HitBird: Enter");
        while (moleStates == MoleStates.HitBird)
        {
            yield return 0;
        }
        Debug.Log("HitBird: Exit");
        NextState();
    }
    IEnumerator WaterHitState()
    {
        Debug.Log("WaterHit: Enter");
        while (moleStates == MoleStates.WaterHit)
        {
            yield return 0;
        }
        Debug.Log("WaterHit: Exit");
        NextState();
    }

    void Start()
    {
        NextState();
    }

  

    void NextState()
    {
        string methodName = moleStates.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }


}
