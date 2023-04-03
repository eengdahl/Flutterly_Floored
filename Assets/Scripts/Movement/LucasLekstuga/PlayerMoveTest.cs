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
    private Vector2 mouseDelta;
    private Vector3 inputsXZ;

    //public Transform MainCamera;

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
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    private void FixedUpdate()
    {

        SpeedControl();


        Vector3 moveDirection = (transform.forward.normalized * moveInput.y) + (Camera.main.transform.right.normalized * moveInput.x);
        inputsXZ = moveDirection.normalized;


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

    private void LateUpdate()
    {
        var forward = Camera.main.transform.forward;
        forward.y = 0;
        transform.forward = forward;
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limits speed on ground
        if (jump.isGrounded && flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        //limits speed in air
        else if (!jump.isGrounded && flatVel.magnitude > (maxSpeed * airControl))
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }
}
