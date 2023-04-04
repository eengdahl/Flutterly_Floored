using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float airControl;
    [SerializeField] float turnSpeed = 10f;
    
    private float turnSpeedMultiplier;
    private Rigidbody rb;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private Vector2 mouseDelta;
    private Vector3 inputsXZ;
    private Vector3 targetDirection;
    private Quaternion freeRotation;
    private Camera mainCamera;
    private Vector3 normalizedVel;

    PlayerWind playerWindScrips;

    //public Transform MainCamera;

    private JumpTest jump;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerWindScrips = GetComponent<PlayerWind>();
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
        normalizedVel = rb.velocity;
        normalizedVel.y = 0;


        if (jump.isGrounded && targetDirection.magnitude > 0.1f)
        {
            rb.AddForce(targetDirection * speed * 10f, ForceMode.Force);
        }
        //in air
        else if (!jump.isGrounded)
        {
            if (!playerWindScrips.inWindZone)
            {
                rb.AddForce(targetDirection * speed * 10f * airControl, ForceMode.Force);

            }
            else
            {
                rb.AddForce(new Vector3(inputsXZ.x, 0f, 0f) * speed * 10f * airControl, ForceMode.Force);
            }
        }

        Vector3 moveDirection = transform.forward.normalized * moveInput.y + (Camera.main.transform.right.normalized * moveInput.x);
        inputsXZ = new Vector3(moveInput.x, 0f, moveInput.y);
        //+(Camera.main.transform.right.normalized * moveInput.x)

        UpdateTargetDirection();

        if (inputsXZ != Vector3.zero && targetDirection.magnitude > 0.1f)
        {
            transform.LookAt(transform.position + normalizedVel, Vector3.up);
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
        }
        
        //on ground
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
    public virtual void UpdateTargetDirection()
    {
        turnSpeedMultiplier = 1f;
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        //get the right-facing direction of the referenceTransform
        var right = mainCamera.transform.TransformDirection(Vector3.right);

        // determine the direction the player will face based on input and the referenceTransform's right and forward directions
        targetDirection = inputsXZ.x * right + inputsXZ.z * forward;
    }
}