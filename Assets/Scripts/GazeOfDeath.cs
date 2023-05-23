using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GazeOfDeath : MonoBehaviour
{

    public VitrinBrain2 brain;
    public GameObject visionCone;
    public float distance;
    public LayerMask player;
    DeathScriptAndCheckPoint playerDeath;
    public bool locker;
    public bool inKillerGazeOfDeath;
    private GameObject objectInGaze;



    private void Start()
    {
        locker = false;
        playerDeath = FindAnyObjectByType<DeathScriptAndCheckPoint>();
    }

    private void Update()
    {
        if(inKillerGazeOfDeath)
        {
            RaycastHit hit;
            Physics.Raycast(visionCone.transform.position, objectInGaze.transform.position - visionCone.transform.position, out hit, distance, player);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                playerDeath = objectInGaze.GetComponent<DeathScriptAndCheckPoint>();
                objectInGaze.GetComponent<Rigidbody>().isKinematic = false;
                playerDeath.Die();
                locker = true;
                Invoke(nameof(PausRay), 1);

            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (locker)
    //    {
    //        return;
    //    }
    //    if(other.CompareTag("Player"))
    //    {
    //        if (locker)
    //        {
    //            return;
    //        }
    //        RaycastHit hit;
    //        Physics.Raycast(visionCone.transform.position, other.transform.position - visionCone.transform.position, out hit,distance, player);
    //        if(hit.collider.gameObject.CompareTag("Player"))
    //        {
    //            playerDeath = other.GetComponent<DeathScriptAndCheckPoint>();
    //            other.GetComponent<Rigidbody>().isKinematic = false;
    //            playerDeath.Die();
    //            locker = true;
    //            Invoke(nameof(PausRay), 1);

    //        }

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(locker) 
            { 
                return; 
            }

            objectInGaze = other.gameObject;
            inKillerGazeOfDeath = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (locker)
            {
                return;
            }

            objectInGaze = null;
            inKillerGazeOfDeath = false;
        }
    }

    void PausRay()
    {
        locker = false;
    }
}
