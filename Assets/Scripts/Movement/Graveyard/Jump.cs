using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour, IMoveSpeedListener
{
    public float jumpForce;
    public float jumpChargeMultiplier;
    public float timeForChargedJump;
    public bool isDoubleJumping;
    public float coyoteTime;
    public float coyoteTimeCounter;

    public bool isJumping;
    private bool isJumpPressed;
    public float timePressed;



    [SerializeField]
    private bool isGrounded;
    private Rigidbody rb;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        MovementCommunicator.instance.AddMoveListener(this);
    }

    private void OnDisable()
    {
        MovementCommunicator.instance.RemoveMoveListener(this);

    }

    public void OnValueChanged(float speed)
    {
        //Debug.Log(speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumpPressed)
        {
            timePressed += Time.deltaTime;
        }

        if (isJumping && timePressed > timeForChargedJump)
        {
            ChargedJump();
            isJumpPressed = false;
            timePressed = 0;
        }
        //else if(timeForChargedJump > timeForChargedJump)
        //{
            
        //}

        if (isGrounded && !isJumping)
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

            if(isGrounded)
            {
                NormalJump();
                isJumping = true;
                isJumpPressed= true;
            }
        }

        if(input.canceled && isGrounded)
        {
            //if(timePressed > timeForChargedJump)
            //{
            //    ChargedJump();
            //    isJumping = true;
            //}    
            //else
            //{
            //    NormalJump();
            //    isJumping = true;
            //}

            //isGrounded = false;
            isJumpPressed = false;
            timePressed = 0;
        }
    }

    private void NormalJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ChargedJump()
    {
        rb.AddForce(Vector3.up * jumpForce * jumpChargeMultiplier, ForceMode.Impulse);
    }

    public void SecondJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce + new Vector3(0, rb.velocity.y,0), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //timePressed = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            isDoubleJumping = false;
            MovementCommunicator.instance.NotifyGroundedListeners(isGrounded);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            MovementCommunicator.instance.NotifyGroundedListeners(isGrounded);
        }
    }
}
