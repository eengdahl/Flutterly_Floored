using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class HitZones : MonoBehaviour
{
    public GameObject[] hitzones;
    public Animator attackAnimator;
    public CatAMoleBrain gamestate;
    public GameObject player;


    public float attackSpeed;
    public float lazySpeed;
    public float notAttackingTimer;
    public float StrikeDistance;        //distance between player and cat to trigger attack
    public bool withinStrikeDistance;   //If player is within striking distance
    public bool catIsActive;           //to stop cat from resetting attackanimations before cat has started attacking


    private float currentDistance;      //Current distance between player and cat
    private GameObject closestHitzone;
    private string animationTrigger;
    private float distanceClosestPosition;
    private bool isAttacking;
    private bool isCharging;
    private bool isPlayerInZone;
    private bool isAnimTriggered;


    // Start is called before the first frame update
    void Start()
    {
        CheckClosestZone();     //Check closest hitzone to player
        gamestate.moleState = MoleStates.Charge;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckClosestZone();
        DistanceToPlayer();


        if (gamestate.moleState == MoleStates.Attack && !isAttacking)
        {
            CheckClosestZone();
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
        if (isAttacking && !isAnimTriggered && withinStrikeDistance)
        {
            Debug.Log("Runs attack code");
            isCharging = false;
            isAnimTriggered = true;
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

            attackAnimator.SetBool(animationTrigger, true);
        }

        if(isCharging)
        {
            isAnimTriggered = false;
            attackAnimator.SetBool("AttackCanBeTriggered", true);
            //attackAnimator.SetBool(animationTrigger, false);
            attackAnimator.SetBool("Charging", true);
        }
        else
        {
            attackAnimator.SetBool("Charging", false);
        }

        if (gamestate.moleState == MoleStates.Charge && isAttacking)
        {
            if (catIsActive)
            {
                attackAnimator.SetBool(animationTrigger, false);
            }

            isCharging = true;
            isAttacking = false;
        }
    }

    private void PlayerVisable()
    {
        RaycastHit hit;
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
        //Debug.Log(closestHitzone.name);

    }


    //Calculates the distance between cat and player and sets if player is close enough for attack
    public void DistanceToPlayer()
    {
        currentDistance = (player.transform.position - gamestate.gameObject.transform.position).magnitude;

        if(currentDistance < StrikeDistance)
        {
            withinStrikeDistance = true;
        }
        else
        {
            withinStrikeDistance = false;
        }
    }

}
