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
    [SerializeField] private bool isDoubleJumping;
    [SerializeField] private bool hasDoubleJumped;

    public bool canDoubleJump;
    public float doubleJumpMultiplier;
    public float coyoteTime;
    public float coyoteTimeCounter;


    [SerializeField]
    public bool isGrounded;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (isDoubleJumping)
            Glide();
    }

    public void ButtonInput(InputAction.CallbackContext input)
    {

        if (input.started)
        {
            if (isGrounded && coyoteTimeCounter > 0 && readyToJump)
            {
                coyoteTimeCounter = 0;
                Jump();

                isGrounded = false;
                readyToJump = false;

                //Invoke(nameof(ResetJump), jumpCooldown);
            }
        }



        if (input.canceled && !isGrounded && !hasDoubleJumped)
        {
            canDoubleJump = true;
        }

        if (input.started && !isGrounded && !hasDoubleJumped && canDoubleJump)
        {
            DoubleJump();
        }

        if (input.canceled && !isGrounded && isDoubleJumping)
            isDoubleJumping = false;

    }

    private void Jump()
    {
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            readyToJump = true;
            canDoubleJump = false;
            isDoubleJumping = false;
            hasDoubleJumped = false;
        }
    }

    public void DoubleJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * (jumpForce * doubleJumpMultiplier), ForceMode.Impulse);
        canDoubleJump = false;
        hasDoubleJumped = true;
        isDoubleJumping = true;
    }

    public void Glide()
    {
        rb.AddForce(transform.up * glideForce, ForceMode.Force);
    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
