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
    [SerializeField] public bool gliding;
    [SerializeField] public bool hasCanceledGlide;

    [SerializeField] private Animator anim;
    [SerializeField] private float crashRayDistance;
    private float crashNotGroundedTimer;

    public bool canCrash;
    public bool canGlide;
    public float glideTime;
    public float coyoteTime;
    public float airTime;
    public float coyoteTimeCounter;
    private float fallSpeed;
    private float groundCheckDistance;
    private GameObject leftFoot;
    private GameObject rightFoot;
    private GameObject crashRayObject;
    RaycastHit leftFootHit;
    RaycastHit rightFootHit;

    PlayerWind playerWindsScript;
    KitchenFan kitchenFan;

    [SerializeField]
    BirdCableMovement climScript;
    public bool isGrounded;
    private Rigidbody rb;


    void Awake()
    {

        leftFoot = GameObject.Find("LeftFootTransform");
        rightFoot = GameObject.Find("RightFootTransform");
        crashRayObject = GameObject.Find("CrashRayTransform");
        playerWindsScript = GetComponent<PlayerWind>();
        kitchenFan = GameObject.Find("KitchenFan").GetComponent<KitchenFan>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //conflicts with climb. should check = if climbing, return
        RaycastHit crashHit;
        if (Physics.Raycast(crashRayObject.transform.position, -crashRayObject.transform.up, out crashHit, crashRayDistance)
            && crashHit.collider != null && crashHit.collider.CompareTag("Ground"))
        {
            if (crashNotGroundedTimer >= 1 && crashNotGroundedTimer <= 2.5f)
            {
                anim.SetTrigger("SmoothCrash");
            }
            if (crashNotGroundedTimer >= 2.5f) //old 1.5f
            {
                anim.SetTrigger("Crash");
            }
            crashNotGroundedTimer = 0f;
        }
        else
        {
            if (!climScript.isClimbing)
            {
                crashNotGroundedTimer += Time.deltaTime;
            }
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

        if (Input.GetKey(KeyCode.Space) && canGlide && rb.velocity.y < 0)
        {
            if (!gliding)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            }
            gliding = true;
        }
    }

    void FixedUpdate()
    {
        //RayCasts grounded
        groundCheckDistance = 0.2f;
        Debug.DrawLine(leftFoot.transform.position, leftFoot.transform.position + new Vector3(0, -groundCheckDistance, 0), Color.red);
        Debug.DrawLine(rightFoot.transform.position, rightFoot.transform.position + new Vector3(0, -groundCheckDistance, 0), Color.blue);
        bool rayCastHit = false;

        if (Physics.Raycast(leftFoot.transform.position, -leftFoot.transform.up, out leftFootHit, groundCheckDistance))
        {
            if (leftFootHit.collider != null && leftFootHit.collider.CompareTag("Ground"))
            {
                canCrash = false;
                hasCanceledGlide = false;
                isGrounded = true;
                readyToJump = true;
                canGlide = false;
                Invoke(nameof(ReSetCrash), 1);
                rayCastHit = true;
            }
        }

        if (Physics.Raycast(rightFoot.transform.position, -rightFoot.transform.up, out rightFootHit, groundCheckDistance))
        {
            if (rightFootHit.collider != null && rightFootHit.collider.CompareTag("Ground"))
            {
                hasCanceledGlide = false;
                isGrounded = true;
                readyToJump = true;
                canGlide = false;
                Invoke(nameof(ReSetCrash), 1);
                rayCastHit = true;
            }
        }
        if (!rayCastHit)
        {
            if (!hasCanceledGlide && coyoteTimeCounter < 0)
            {
                canGlide = true;
            }
            isGrounded = false;
        }

        if (gliding)
            Glide();

        if (!isGrounded)
        {
            airTime += Time.deltaTime;
        }

        if (isGrounded)
        {
            airTime = 0;
        }
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


        //Cancels when you stop pressing space
        if (input.canceled && gliding)
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
        if (!playerWindsScript.inWindZone && !kitchenFan.inGawkArea)
        {
            glideTime += Time.deltaTime;
            rb.AddForce(transform.up * glideForce, ForceMode.Acceleration);
        }
        if (playerWindsScript.inWindZone)
        {
            rb.AddForce(transform.up * windTunnelAscend, ForceMode.Force);
        }
        if (kitchenFan.inGawkArea)
        {
            glideTime += Time.deltaTime;
            rb.AddForce(transform.up * (glideForce * kitchenFan.glideMultiplier), ForceMode.Acceleration);
        }
    }

    public void CancelGlide()
    {
        if (!playerWindsScript.inWindZone)
        {
            //anim.SetTrigger("GlideFail");

            gliding = false;
            glideTime = 0f;
            hasCanceledGlide = true;
            canCrash = true;
            canGlide = false;

        }
    }

    private void ReSetCrash()
    {
        canCrash = false;
    }
}
