using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForceMovement : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 10f;
    [SerializeField]
    private float airSpeed = 5f;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float maxAirSpeed = 5f;
    [SerializeField]
    private float groundFriction = 0.5f;
    [SerializeField]
    private float airFriction = 0.05f;

    private Rigidbody rb;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private Vector3 decelerationVector;

    private Jump jump;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Vector3 tempvector = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(moveInput.x, 0, moveInput.y);

        if (jump.isGrounded)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                decelerationVector = new Vector3(rb.velocity.x * groundFriction, 0, rb.velocity.z * groundFriction);
                rb.AddForce(movementVector.normalized * acceleration - decelerationVector, ForceMode.Acceleration);
            }
            else
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
                decelerationVector = new Vector3(rb.velocity.x * groundFriction, 0, rb.velocity.z * groundFriction);
                rb.AddForce(movementVector.normalized * maxSpeed - decelerationVector, ForceMode.Acceleration);
            }
        }
        else
        {
            decelerationVector = new Vector3(rb.velocity.x * airFriction, 0, rb.velocity.z * airFriction);

            if (rb.velocity.magnitude < maxAirSpeed)
            {
                rb.AddForce(movementVector.normalized * airSpeed - decelerationVector, ForceMode.Acceleration);
            }
            else
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxAirSpeed);
                rb.AddForce(movementVector.normalized * maxAirSpeed - decelerationVector, ForceMode.Acceleration);
            }
        }
    }
}