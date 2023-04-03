using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchControls : MonoBehaviour
{

    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }



    public void SwitchToAir()
    {
        playerInput.actions.FindActionMap("Floor").Disable();
        playerInput.actions.FindActionMap("WindZone").Enable();
    }
    public void SwitchToFloor()
    {
        playerInput.actions.FindActionMap("Floor").Enable();
        playerInput.actions.FindActionMap("WindZone").Disable();
    }
}
