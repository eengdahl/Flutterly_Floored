using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    public bool canGlide;
    public float glideTime;
    public float coyoteTime;
    public float coyoteTimeCounter;
    private float groundCheckDistance = 0.1f;
    //0.387

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
        //groundCheckDistance = gameObject.GetComponent<BoxCollider>().size.y / 2 + 0.1f;
        ////Debug.DrawLine(transform.GetChild(0).position, Vector3.down, Color.red);
        //Debug.DrawLine(transform.position + gameObject.GetComponent<BoxCollider>().center, transform.position + gameObject.GetComponent<BoxCollider>().center + new Vector3(0, -groundCheckDistance, 0) , Color.red);
        ////Debug.DrawRay(transform.position, -transform.up * groundCheckDistance);
        ////RayCasts grounded
        //RaycastHit leftFoot;
        //if (Physics.Raycast(transform.position + gameObject.GetComponent<BoxCollider>().center, -transform.up, out leftFoot, groundCheckDistance))
        //{
        //    if (leftFoot.collider.tag == "Ground")
        //        isGrounded = true;
        //}
        //else
        //{
        //    isGrounded = false;
        //}

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
        CompleteJump(input);
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
        if (!playerWindsScript.inWindZone)
        {
            glideTime += Time.deltaTime;
            rb.AddForce(transform.up * (glideTime * glideForce), ForceMode.Acceleration);
            //rb.useGravity = false;
        }
        else if (playerWindsScript.inWindZone)
        {
            //rb.AddForce(transform.up * windTunnelAscend, ForceMode.Force);
        }
    }

    public void CancelGlide()
    {
        if (!playerWindsScript.inWindZone)
        {
            gliding = false;
            //rb.useGravity = true;
            glideTime = 0.1f;
            hasCanceledGlide = true;
            canGlide = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            readyToJump = true;
            canGlide = false;
            CancelGlide();
            hasCanceledGlide = false;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    public void CompleteJump(InputAction.CallbackContext input)
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
        //Reset jump to keep jumping while space is pressed
        if (input.performed && !readyToJump)
        {
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //Enables glide after jumping
        if (input.canceled && !isGrounded && !hasCanceledGlide)
        {
            canGlide = true;
        }

        //Gliding input events

        //Gliding starts if pressing space in air
        if (input.started && !isGrounded && canGlide)
        {
            //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            gliding = true;
        }
        //Cancels when you stop pressing space
        if (input.canceled && !isGrounded && gliding)
        {
            CancelGlide();
        }
    }
}
