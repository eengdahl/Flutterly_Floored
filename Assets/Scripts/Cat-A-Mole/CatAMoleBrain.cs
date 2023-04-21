using HoudiniEngineUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoleStates
{
    Idle,
    Charge,
    Attack,
    HitBird,
    WaterHit
}

public class CatAMoleBrain : MonoBehaviour
{
   

    public MoleStates moleState;
    public GameObject Cat;
    private float catTimerDone;
    public bool hitting = false;

    void Awake()
    {
        catTimerDone = 3.0f;
        NextState();
    }


    IEnumerator IdleState()
    {
        //Debug.Log("Idle: Enter");
        while (moleState == MoleStates.Idle)
        {
            yield return 0;
        }
        //Debug.Log("Idle: Exit");
        NextState();
    }

    IEnumerator ChargeState()
    {
        //Debug.Log("Charge: Enter");
        while (moleState == MoleStates.Charge)
        {
            catTimerDone = Random.Range(1, 2);
            yield return new WaitForSeconds(catTimerDone);

            moleState = MoleStates.Attack;

            yield return 0;
        }
        //Debug.Log("Charge: Exit");
        NextState();
    }

    IEnumerator AttackState()
    {
        //Debug.Log("Attack: Enter");
        while (moleState == MoleStates.Attack)
        {
            if (!hitting) hitting = true;

            yield return new WaitForSeconds(1);
            hitting = false;
            moleState = MoleStates.Charge;
            NextState();
            yield return 0;
        }
        //Debug.Log("Attack: Exit");
    }

    IEnumerator HitBirdState()
    {
        //Debug.Log("HitBird: Enter");
        while (moleState == MoleStates.HitBird)
        {
            yield return 0;
        }
        NextState();
        //Debug.Log("HitBird: Exit");
    }

    IEnumerator WaterHitState()
    {
        //Debug.Log("WaterHit: Enter");
        while (moleState == MoleStates.WaterHit)
        {
            yield return 0;
        }
        NextState();
        //Debug.Log("WaterHit: Exit");
    }


    void NextState()
    {
        string methodName = moleState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
