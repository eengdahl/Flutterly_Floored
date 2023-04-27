using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeOfDeath : MonoBehaviour
{

    public VitrinBrain brain;
    public GameObject visionCone;
    public float distance;
    public LayerMask player;
    DeathScriptAndCheckPoint playerDeath;



    private void Start()
    {
        playerDeath = FindAnyObjectByType<DeathScriptAndCheckPoint>();
    }



    private void OnTriggerStay(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            RaycastHit hit;
            Physics.Raycast(visionCone.transform.position, other.transform.position - visionCone.transform.position, out hit,distance, player);

            if(hit.collider.gameObject.CompareTag("Player"))
            {
                playerDeath = other.GetComponent<DeathScriptAndCheckPoint>();
                other.GetComponent<Rigidbody>().isKinematic = false;
                playerDeath.Die();

            }

        }
    }
}
