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

    public bool canGlide;
    public float glideTime;
    public float coyoteTime;
    public float coyoteTimeCounter;
    private float fallSpeed;
    private float groundCheckDistance;
    private GameObject leftFoot;
    private GameObject rightFoot;

    PlayerWind playerWindsScript;

    [SerializeField]
    public bool isGrounded;
    private Rigidbody rb;


    void Awake()
    {
        leftFoot = GameObject.Find("LeftFootTransform");
        rightFoot = GameObject.Find("RightFootTransform");
        playerWindsScript = GetComponent<PlayerWind>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //raycastOffset = new Vector3(gameObject.GetComponent<BoxCollider>().size.x, 0, gameObject.GetComponent<BoxCollider>().size.z);
        //RayCasts grounded
        groundCheckDistance = 0.1f;
        Debug.DrawLine(leftFoot.transform.position, leftFoot.transform.position + new Vector3(0, -groundCheckDistance, 0), Color.red);
        Debug.DrawLine(rightFoot.transform.position, rightFoot.transform.position + new Vector3(0, -groundCheckDistance, 0), Color.blue);
        RaycastHit leftFootHit;
        RaycastHit rightFootHit;
        if (Physics.Raycast(leftFoot.transform.position, -leftFoot.transform.up, out leftFootHit, groundCheckDistance))
        {
            if (leftFootHit.collider.CompareTag("Ground"))
            {
                isGrounded = true;
                hasCanceledGlide = false;
                readyToJump = true;
                canGlide = false;
            }
        }
        if (Physics.Raycast(rightFoot.transform.position, -rightFoot.transform.up, out rightFootHit, groundCheckDistance))
        {
            if (rightFootHit.collider.tag == "Ground")
            {
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
            isGrounded = false;
        }

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
        //Jump if you're on ground or during coyoteTime
        if (coyoteTimeCounter > 0f && input.started)
        {
            Jump();
        }
        //Reset jump to keep jumping while space is pressed
        if (input.action.IsInProgress() && !readyToJump)
        {
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //Enables glide after jumping
        if (input.canceled && rb.velocity.y > 0f /*!isGrounded && !hasCanceledGlide*/)
        {
            coyoteTimeCounter = 0f;
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

    //Function for jumping, adds force in upwards direction
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
 
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
