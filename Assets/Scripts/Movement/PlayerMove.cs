using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float airSpeed;
    [SerializeField]
    private float maxSpeed;
    private Rigidbody rbody;
    private PlayerControls playerControls;
    private Vector2 moveInput;

    private Jump jump;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rbody = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
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

    private void FixedUpdate()
    {
        Vector3 tempvector = new Vector3(moveInput.x, 0, moveInput.y);

        Debug.Log(rbody.velocity.magnitude);

        if (jump.isGrounded)
        {
            rbody.velocity = tempvector * speed;
        }
        else
        {
            Debug.Log(rbody.velocity.magnitude);

            //if (rbody.velocity.magnitude < maxSpeed)
            //{
            //    Debug.Log(rbody.velocity.magnitude);
            //    rbody.velocity = tempvector * speed;
            //}
        }
       
        //moveInput = playerControls.Floor.Move.ReadValue<Vector2>();
        //Vector3 tempvector = new Vector3(moveInput.x, 0, moveInput.y);
        //rbody.velocity = tempvector * speed;
    }
}
