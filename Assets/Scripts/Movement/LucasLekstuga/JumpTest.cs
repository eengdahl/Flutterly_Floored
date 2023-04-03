using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpTest : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float glideForce;
    [SerializeField] private bool readyToJump;
    [SerializeField] private bool Gliding;
    [SerializeField] private bool hasDoubleJumped;

    public bool canGlide;
    public float glideTime;
    public float coyoteTime;
    public float coyoteTimeCounter;
    private float groundCheckDistance = 1;


    [SerializeField]
    public bool isGrounded;
    private Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //RayCasts grounded
        RaycastHit leftFoot;
        if (Physics.Raycast(transform.position, -transform.up, out leftFoot, groundCheckDistance) && !isGrounded)
        {
            if (leftFoot.collider.tag == "Ground")
                isGrounded = true;
            else
                isGrounded = false;
        }

        //Resets coyoteTime when on ground and when off ground start counting down
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (glideTime >= 1)
            CancelGlide();

    }

    void FixedUpdate()
    {
        if (Gliding)
            Glide();
    }

    //Input events for Spacebar (Jump key)
    public void ButtonInput(InputAction.CallbackContext input)
    {
        //Jump if you're on ground or during coyoteTime
        if (input.started && isGrounded || coyoteTimeCounter > 0)
        {
            if (input.action.IsInProgress())
            {
                coyoteTimeCounter = 0;
                Jump();

                isGrounded = false;
                readyToJump = false;
            }

        }
        if (input.performed && !readyToJump)
        {
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (input.canceled && !isGrounded && !hasDoubleJumped)
        {
            canGlide = true;
        }
        
        //Gliding input events

        //Gliding starts if pressing space in air
        if (input.started && !isGrounded && canGlide)
        {
            Gliding = true;
        }
        //Cancels when you stop pressing space
        if (input.canceled && !isGrounded && Gliding)
            Gliding = false;
    }

    //Function for jumping, adds force in upwards direction and boosts player in moving direction
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void Glide()
    {
        glideTime += Time.deltaTime;
        rb.AddForce(transform.up * (glideForce * glideTime), ForceMode.Acceleration);
    }

    public void CancelGlide()
    {
        Gliding = false;
        glideTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            readyToJump = true;
            canGlide = false;
            Gliding = false;
            hasDoubleJumped = false;
            glideTime = 0;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
