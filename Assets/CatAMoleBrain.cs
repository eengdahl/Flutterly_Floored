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

    public MoleStates currentMoleState;
    public GameObject Cat;
    private float catTimerDone;
    public bool hitting = false;

    void Awake()
    {
        catTimerDone = 3.0f;
        NextState();
    }

    //Ändra currentMoleState = MolStates.x så byts State automatiskt 
    IEnumerator IdleState()
    {
        Debug.Log("Idle: Enter");
        while (currentMoleState == MoleStates.Idle)
        {
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        NextState();
    }

    IEnumerator ChargeState()
    {
        Debug.Log("Charge: Enter");
        while (currentMoleState == MoleStates.Charge)
        {
            //Timer innan scriptet fortsätter nedanför 
            yield return new WaitForSeconds(catTimerDone);

            currentMoleState = MoleStates.Attack;

            yield return 0;
        }
        Debug.Log("Charge: Exit");
        NextState();
    }

    IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        while (currentMoleState == MoleStates.Attack)
        {
            if (!hitting) hitting = true;

            yield return new WaitForSeconds(3);
            hitting = false;
            currentMoleState = MoleStates.Charge;
            NextState();
            yield return 0;
        }
        Debug.Log("Attack: Exit");
    }

    IEnumerator HitBirdState()
    {
        Debug.Log("HitBird: Enter");
        while (currentMoleState == MoleStates.HitBird)
        {
            yield return 0;
        }
        Debug.Log("HitBird: Exit");
        NextState();
    }

    IEnumerator WaterHitState()
    {
        Debug.Log("WaterHit: Enter");
        while (currentMoleState == MoleStates.WaterHit)
        {
            yield return 0;
        }
        Debug.Log("WaterHit: Exit");
        NextState();
    }


    void NextState()
    {
        string methodName = currentMoleState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
