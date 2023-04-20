using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class HitZones : MonoBehaviour
{
    public GameObject[] hitzones;
    public Animator pawAnimator;
    public CatAMoleBrain gamestate;
    public GameObject player;
    //public GameObject leftPaw;
    //public GameObject rightPaw;
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
    //private Vector3 leftPawStartPosition;
    //private Vector3 rightPawStartPosition;


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
        
        if (gamestate.moleState == MoleStates.Attack && !isAttacking)
        {
            CheckClosestZone();
            CheckPlayerInZone();
            isAttacking= true;
            isCharging = false;

            //What happens to player if in a hitzone
            if (closestHitzone != null && isPlayerInZone)
            {
                //Debug.Log("bird got hit in Zone:" +closestHitzone.name);
            }
        }

        //Make cat attack with paws
        if (isAttacking && !isAnimTriggered)
        {
            isCharging = true;
            if (closestHitzone.name == "Hitzone1")
            {
                animationTrigger = "ShortRight";
                //pawAnimator.SetBool(animationTrigger, true);
                //leftPaw.GetComponent<Rigidbody>().AddForce(transform.up * 2, ForceMode.Impulse);
                //leftPaw.transform.position = Vector3.MoveTowards(leftPaw.transform.position, closestHitzone.transform.position, Time.deltaTime * attackSpeed);
            }


            if (closestHitzone.name == "Hitzone2")
            {
                animationTrigger = "LongRight";
                //pawAnimator.SetTrigger(animationTrigger);
                //leftPaw.GetComponent<Rigidbody>().AddForce(transform.up * 2, ForceMode.Impulse);
                //leftPaw.transform.position = Vector3.MoveTowards(leftPaw.transform.position, closestHitzone.transform.position, Time.deltaTime * attackSpeed);
            }

            if (closestHitzone.name == "Hitzone3")
            {
                animationTrigger = "SuperLongLeft";
                //pawAnimator.SetTrigger(animationTrigger);
            }

            if (closestHitzone.name == "Hitzone4")
            {
                animationTrigger = "SuperLongLeft";
                //pawAnimator.SetTrigger(animationTrigger);
            }

            pawAnimator.SetBool(animationTrigger, true);

            pawAnimator.SetBool("AttackCanBeTriggered", false);



            //else
            //{
            //    //rightPaw.transform.position = Vector3.MoveTowards(rightPaw.transform.position, closestHitzone.transform.position, Time.deltaTime * attackSpeed);
            //}
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

        //Pull back pawns
        if (gamestate.moleState == MoleStates.Charge && isCharging)
        {
            isCharging = true;
            isAttacking = false;
            pawAnimator.ResetTrigger(animationTrigger);

            

            if (closestHitzone.CompareTag("LeftHitZone"))
            {
                //leftPaw.transform.position = Vector3.Lerp(leftPaw.transform.position, leftPawStartPosition, Time.deltaTime * lazySpeed);
            }
            else
            {
                //rightPaw.transform.position = Vector3.Lerp(rightPaw.transform.position, rightPawStartPosition, Time.deltaTime * lazySpeed);
            }

            //Debug.Log("rightpaw start pos: " + leftPawStartPosition);


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
        //Debug.Log(closestHitzone.name);

    }

}
