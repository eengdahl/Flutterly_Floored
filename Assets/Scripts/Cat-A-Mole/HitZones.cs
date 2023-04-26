using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class HitZones : MonoBehaviour
{
    public GameObject cat;
    public GameObject[] hitzones;
    public Animator pawAnimator;
    public CatAMoleBrain gamestate;
    public GameObject player;
    public float attackSpeed;
    public float lazySpeed;
    public float notAttackingTimer;

    private string animationTrigger;
    public GameObject closestHitzone;
    private float distanceClosestPosition;
    public bool isAttacking;
    public bool isCharging;
    public bool isPlayerInZone;
    private bool isAnimTriggered;


    // Start is called before the first frame update
    void Start()
    {
        //leftPawStartPosition = leftPaw.transform.position;
        //rightPawStartPosition = rightPaw.transform.position;
        CheckClosestZone();     //Check closest hitzone to player
        gamestate.moleState = MoleStates.Charge;
    }

    // Update is called once per frame
    void Update()
    {
        CheckClosestZone();

        if (gamestate.moleState == MoleStates.Attack && !isAttacking)
        {
            //CheckClosestZone();
            CheckPlayerInZone();
            isAttacking= true;
            isCharging = false;

           

            //What happens to player if in a hitzone
            if (closestHitzone != null)
            {
                
                //Debug.Log("bird got hit in Zone:" +closestHitzone.name);
            }
        }

        if(closestHitzone != null)
        {
           
        }

        //Make cat attack
        if (isAttacking && !isAnimTriggered)
        {
            isCharging = false;
            if (closestHitzone.name == "Hitzone1")
            {
                animationTrigger = "ShortRight";
            }

            if (closestHitzone.name == "Hitzone4")
            {
                animationTrigger = "ShortLeft";
            }

            if (closestHitzone.name == "Hitzone2")
            {
                animationTrigger = "LongRight";
            }

            if (closestHitzone.name == "Hitzone5")
            {
                animationTrigger = "LongLeft";
            }

            if (closestHitzone.name == "Hitzone3")
            {
                animationTrigger = "SuperLongRight";
            }

            if (closestHitzone.name == "Hitzone6")
            {
                animationTrigger = "SuperLongLeft";
            }
        }

        if(isCharging)
        {
            isAnimTriggered = false;
            pawAnimator.SetBool("AttackCanBeTriggered", true);
            pawAnimator.SetBool(animationTrigger, true);
            pawAnimator.SetBool("Charging", true);
        }
        else
        {
            pawAnimator.SetBool("Charging", false);
        }

        if (gamestate.moleState == MoleStates.Charge && isAttacking)
        {
            isCharging = true;
            isAttacking = false;
            pawAnimator.ResetTrigger(animationTrigger);
        }
    }



    private void CheckPlayerInZone()
    {
        if (closestHitzone != null)
        {
            isPlayerInZone = closestHitzone.GetComponent<Hitzone>().isPlayerInZone;
        }
    }

    //Checks and sets which zone is closest to the player
    private void CheckClosestZone()
    {
        closestHitzone = null;
        distanceClosestPosition= 0;
        foreach (GameObject hitzone in hitzones)
        {
            float tmpDistance = (hitzone.transform.position - player.transform.position).magnitude;

            if(closestHitzone == null || tmpDistance < distanceClosestPosition)
            {
                closestHitzone = hitzone;
                distanceClosestPosition = tmpDistance;
            }     
        }
        Debug.Log(closestHitzone.name);

    }

}
