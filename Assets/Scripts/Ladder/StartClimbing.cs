using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClimbing : MonoBehaviour
{

    ClimbAlongScript climbAlongScript;
    BirdCableMovement CableMovement;

    int index;

    void Start()
    {
        CableMovement = FindAnyObjectByType<BirdCableMovement>();
        climbAlongScript = GetComponentInParent<ClimbAlongScript>();

        FindIndexInList();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CableMovement.readyToClimb)
        {
            if (!CableMovement.isClimbing)
            {
                other.GetComponent<BirdCableMovement>().cableplant = climbAlongScript;
                CableMovement.currentCableSegment = index;
                CableMovement.EnableClimbing();
                other.GetComponent<SwitchControls>().SwitchToClimbing();
                other.gameObject.transform.position = transform.position;

            }



        }
    }

    void FindIndexInList()
    {
        index = climbAlongScript.points.IndexOf(transform);

    }



}
