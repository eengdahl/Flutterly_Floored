using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartClimbing : MonoBehaviour
{
    SwitchControls switchControls;
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
        switchControls = FindAnyObjectByType<SwitchControls>();
        input = new PlayerControls();
        CableMovement = FindAnyObjectByType<BirdCableMovement>();
        climbAlongScript = GetComponentInParent<ClimbAlongScript>();

        FindIndexInList();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CableMovement.readyToClimb)
            {
                if (input.Floor.Drag.IsPressed())
                {
                    if (!CableMovement.isClimbing)
                    {
                        CableMovement.cableplant = climbAlongScript;
                        CableMovement.currentCableSegment = index;
                        CableMovement.EnableClimbing();
                        switchControls.SwitchToClimbing();
                        other.gameObject.transform.position = transform.position;
                        if (climbAlongScript.rotationStartLocked)
                        {
                            other.transform.rotation = Quaternion.Euler(climbAlongScript.startRotation);
                        }
                        else
                        {
                            other.transform.rotation = this.transform.rotation;
                        }
                        if (isVertical)
                        {
                            CableMovement.isVertical = true;
                        }
                        else
                        {
                            CableMovement.isVertical = false;
                        }

                    }

                }
            }

        }
    }

    void FindIndexInList()
    {
        index = climbAlongScript.points.IndexOf(transform);

    }



}
