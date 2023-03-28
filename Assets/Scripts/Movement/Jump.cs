using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    public float jumpForce;
    public float jumpChargeMultiplier;
    public float timeForChargedJump;
    public bool isJumpPressed;
    public float timePressed;
    public bool isDoubleJumping;
    public float coyoteTime;
    public float coyoteTimeCounter;


    [SerializeField]
    private bool isGrounded;
    private Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumpPressed)
        {
            timePressed += Time.deltaTime;
        }

        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void ButtonInput(InputAction.CallbackContext input)
    {

        if(input.performed)
        {
            if (isGrounded || coyoteTimeCounter > 0)
            {
                isJumpPressed = true;
            }
            
            if(!isGrounded && !isDoubleJumping)
            {
                SecondJump();
                isDoubleJumping = true;
            }
        }

        if(input.canceled && isGrounded)
        {
            if(timePressed > timeForChargedJump)
            {
                ChargedJump();
            }    
            else
            {
                NormalJUmp();
            }

            isGrounded = false;
            isJumpPressed = false;
            timePressed = 0;
        }
    }

    private void NormalJUmp()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ChargedJump()
    {
        rb.AddForce(Vector3.up * jumpForce * jumpChargeMultiplier, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isDoubleJumping = false;
        }
    }

    public void SecondJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
