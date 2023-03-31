using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float airControl;

    private Rigidbody rb;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private Vector3 inputsXZ;


    private JumpTest jump;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<JumpTest>();
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
    }

    private void FixedUpdate()
    {
        SpeedControl();

        inputsXZ = new Vector3(moveInput.x, 0, moveInput.y);

        //on ground
        if (jump.isGrounded)
        {
            rb.AddForce(inputsXZ.normalized * speed * 10f, ForceMode.Force);
        }
        //in air
        else if (!jump.isGrounded)
        {
            rb.AddForce(inputsXZ.normalized * speed * 10f * airControl, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limits speed
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
