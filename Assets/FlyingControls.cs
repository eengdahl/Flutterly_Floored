using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FlyingControls : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 flyInput;
    private Vector3 inputsXZ;


    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void Flying(InputAction.CallbackContext context)
    {
        //Read input from action
        flyInput = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        //convert inputs to vector2
        inputsXZ = new Vector3(flyInput.x, 0f, flyInput.y);

    }
}
