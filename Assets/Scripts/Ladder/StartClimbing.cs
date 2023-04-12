using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartClimbing : MonoBehaviour
{

    ClimbAlongScript climbAlongScript;
    BirdCableMovement CableMovement;
    public bool isVertical;
    int index;
    PlayerControls input = null;
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    private void Awake()
    {
        input = new PlayerControls();
    }
    void Start()
    {
        CableMovement = FindAnyObjectByType<BirdCableMovement>();
        climbAlongScript = GetComponentInParent<ClimbAlongScript>();

        FindIndexInList();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && CableMovement.readyToClimb && input.Floor.Drag.IsPressed())
        {
            if (!CableMovement.isClimbing)
            {
                other.GetComponent<BirdCableMovement>().cableplant = climbAlongScript;
                CableMovement.currentCableSegment = index;
                CableMovement.EnableClimbing();
                other.GetComponent<SwitchControls>().SwitchToClimbing();
                other.gameObject.transform.position = transform.position;
                if (isVertical)
                {
                    other.GetComponent<BirdCableMovement>().isVertical = true;
                }
                else
                {
                    other.GetComponent<BirdCableMovement>().isVertical = false;
                }

            }

        }
    }

    void FindIndexInList()
    {
        index = climbAlongScript.points.IndexOf(transform);

    }



}
