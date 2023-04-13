using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float glideForce;
    [SerializeField] private float windTunnelAscend;

    [SerializeField] private bool readyToJump;
    [SerializeField] private bool gliding;
    [SerializeField] private bool hasCanceledGlide;
    [SerializeField] private bool coyoteTimeEnabled;

    public bool canGlide;
    public float glideTime;
    public float coyoteTime;
    public float coyoteTimeCounter;
    private float fallSpeed;
    private float groundCheckDistance;

    PlayerWind playerWindsScript;

    [SerializeField]
    public bool isGrounded;
    private Rigidbody rb;


    void Awake()
    {
        playerWindsScript = GetComponent<PlayerWind>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //RayCasts grounded
        groundCheckDistance = gameObject.GetComponent<BoxCollider>().size.y / 2 + 0.1f;
        Debug.DrawLine(transform.position + gameObject.GetComponent<BoxCollider>().center, transform.position + gameObject.GetComponent<BoxCollider>().center + new Vector3(0, -groundCheckDistance, 0), Color.red);
        RaycastHit leftFoot;
        if (Physics.Raycast(transform.position + gameObject.GetComponent<BoxCollider>().center, -transform.up, out leftFoot, groundCheckDistance))
        {
            if (leftFoot.collider.tag == "Ground")
            {
                coyoteTimeCounter = coyoteTime;
                coyoteTimeEnabled = true;
                isGrounded = true;
                hasCanceledGlide = false;
                readyToJump = true;
                canGlide = false;
            }
        }
        else
        {
            if(!hasCanceledGlide && coyoteTimeCounter < 0)
            {
                canGlide = true;
            }
            coyoteTimeCounter -= Time.deltaTime;
            isGrounded = false;
        }

        if (glideTime >= 1)
        {
            CancelGlide();
        }
    }

    void FixedUpdate()
    {
        if (gliding)
            Glide();
    }

    //Input events for Spacebar (Jump key)
    public void ButtonInput(InputAction.CallbackContext input)
    {
        //Jump if you're on ground or during coyoteTime
        if (input.started)
        {
            Jump();
        }
        //Reset jump to keep jumping while space is pressed
        if (input.action.IsInProgress() && !readyToJump)
        {
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //Enables glide after jumping
        if (input.canceled && !isGrounded && !hasCanceledGlide)
        {
            coyoteTimeCounter = -1;
        }

        //Gliding input events

        //Gliding starts if pressing space in air
        if (input.started && !isGrounded && canGlide)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            gliding = true;
        }
        //Cancels when you stop pressing space
        if (input.canceled && !isGrounded && gliding)
        {
            CancelGlide();
        }
    }

    //Function for jumping, adds force in upwards direction and boosts player in moving direction
    private void Jump()
    {
        if (readyToJump && coyoteTimeCounter > 0.1f)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
        else if (readyToJump && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
        readyToJump = false;

    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void Glide()
    {
        if (!playerWindsScript.inWindZone)
        {

            glideTime += Time.deltaTime;
            rb.AddForce(transform.up * glideForce, ForceMode.Acceleration);
        }
        else if (playerWindsScript.inWindZone)
        {
            rb.AddForce(transform.up * windTunnelAscend, ForceMode.Force);
        }
    }

    public void CancelGlide()
    {
        if (!playerWindsScript.inWindZone)
        {
            gliding = false;
            glideTime = 0f;
            hasCanceledGlide = true;
            canGlide = false;
        }
    }
}
