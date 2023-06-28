using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartClimbing : MonoBehaviour
{
    SwitchControls switchControls;
    public ClimbAlongScript climbAlongScript;
    BirdCableMovement CableMovement;
    public bool isVertical;
    public int index;

    private void Start()
    {

        climbAlongScript = GetComponentInParent<ClimbAlongScript>();

        FindIndexInList();
    }

    void FindIndexInList()
    {
        index = climbAlongScript.points.IndexOf(transform);

    }



}


