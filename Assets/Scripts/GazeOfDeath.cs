using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeOfDeath : MonoBehaviour
{

    public VitrinBrain brain;
    public GameObject visionCone;
    public float distance;
    public LayerMask player;
    Die dieScript;

    private void Start()
    {
        dieScript=FindAnyObjectByType<Die>();
    }



    private void OnTriggerStay(Collider other)
    {

        //Debug.Log(other.gameObject.name);

        if(other.CompareTag("Player") && brain.isGazing)
        {
            RaycastHit hit;
            Physics.Raycast(visionCone.transform.position, other.transform.position - visionCone.transform.position, out hit,distance, player);
            //Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
                dieScript.KillPlayer();

            }

        }
    }
}
