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
        if (brain.catIsDead)
            return;
        if (inKillerGazeOfDeath)
        {
            RaycastHit hit;
            Physics.Raycast(visionCone.transform.position, objectInGaze.transform.position - visionCone.transform.position, out hit, distance, player);
            //Debug.DrawRay(visionCone.transform.position, objectInGaze.transform.position - visionCone.transform.position, Color.cyan, 500);
            //Debug.Log(hit.transform.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                playerDeath = objectInGaze.GetComponent<DeathScriptAndCheckPoint>();
                objectInGaze.GetComponent<Rigidbody>().isKinematic = false;
                playerDeath.Die();
                objectInGaze = null;
                inKillerGazeOfDeath = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectInGaze = other.gameObject;
            inKillerGazeOfDeath = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectInGaze = null;
            inKillerGazeOfDeath = false;
        }
    }
}
