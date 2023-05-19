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
        //playerInput.actions.FindActionMap("Floor").Disable();
        //playerInput.actions.FindActionMap("Climbing").Disable();
        //playerInput.actions.FindActionMap("WindZoneMovement").Enable();
        playerInput.SwitchCurrentActionMap("WindZoneMovement");

    }
    public void SwitchToFloor()
    {
        //playerInput.actions.FindActionMap("Floor").Enable();
        //playerInput.actions.FindActionMap("Climbing").Disable();
        //playerInput.actions.FindActionMap("WindZoneMovement").Disable();
        playerInput.SwitchCurrentActionMap("Floor");

    }

    public void SwitchToClimbing()
    {
        //playerInput.actions.FindActionMap("Climbing").Enable();
        //playerInput.actions.FindActionMap("WindZoneMovement").Disable();
        //playerInput.actions.FindActionMap("Floor").Disable();
        playerInput.SwitchCurrentActionMap("Climbing");

    }

    public void SwitchToFlying()
    {
        playerInput.SwitchCurrentActionMap("Flying");
    }
    public void SwitchToNoInput()
    {
        playerInput.SwitchCurrentActionMap("NoInput");
    }

}
