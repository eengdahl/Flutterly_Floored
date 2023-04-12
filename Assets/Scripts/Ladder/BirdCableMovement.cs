using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BirdCableMovement : MonoBehaviour
{
    //[SerializeField] GameObject player;
    SwitchControls controllsSwitch;
    PlayerControls input = null;
    public ClimbAlongScript cableplant;
    [SerializeField] Camera cameraa;
    [SerializeField] GameObject birdBody;
    Rigidbody rb;
    Collider boxCollider;
    [SerializeField] PlayerJump jumpScript;
    private InputAction.CallbackContext initialInput;


    Transform localTrans;

    public float forcePower = 20f;
    public float speed = 5f;
    public float rotationSpeedAuto = 10f;
    public float rotationSpeed = 10f;
    public float maxYRot = 90;
    public float minYRot = -90;

    Vector3 bodyAngle = new Vector3(90, 90, 0);
    //Vector3 offSet = new Vector3(0,-1.24f,0);

    public int currentCableSegment = 0;

    public bool isClimbing;
    bool atEnd;
    public bool readyToClimb;

    float maxAngleDiff = 10;



    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    private void Awake()
    {
        input = new PlayerControls();

        atEnd = false;
        localTrans = GetComponent<Transform>();

    }
    private void Start()
    {
        boxCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        controllsSwitch = GetComponent<SwitchControls>();
        readyToClimb = true;

    }
    private void Update()
    {
        //For rotation
        if (!isClimbing) return;

        float horizontalInput = input.Climbing.verticalInput.ReadValue<Vector2>().x;
        //Dont do if input is zero, Approximately i think is good when using gamepad
        if (Mathf.Approximately(horizontalInput, 0f))
        {
            return;
        }
        float targetRotation = horizontalInput * rotationSpeed * Time.deltaTime;

     
        transform.Rotate(0f, targetRotation, 0f);
        LimitRotation();


    }
    void LimitRotation()
    {
        Vector3 playerEulerAngles = localTrans.rotation.eulerAngles;
        if (playerEulerAngles.y > 180)//playerEulerAngles.y = (playerEulerAngles.y > 180) ? playerEulerAngles.y - 360 : playerEulerAngles.y;
        {
            playerEulerAngles.y -= 360;
        }       
        playerEulerAngles.y = Mathf.Clamp(playerEulerAngles.y, minYRot, maxYRot);
        localTrans.rotation = Quaternion.Euler(playerEulerAngles);
    }


    private void FixedUpdate()
    {
        if (!isClimbing) return;


        //W Up
        if (input.Climbing.verticalInput.ReadValue<Vector2>().y > 0)
        {

            // Get the direction from the bird's current position to the next cable point
            Vector3 direction = cableplant.points[currentCableSegment].position - transform.position;
            direction.Normalize();

            // Move the bird along the cable in the current segment's direction
            transform.position += direction * speed * Time.deltaTime;

            // Check if the bird has reached the current segment's end point
            if (Vector3.Distance(transform.position, cableplant.points[currentCableSegment].position) < 0.1f)
            {
                if (currentCableSegment < cableplant.points.Count - 1)
                {
                    // Move to the next segment of the cable
                    currentCableSegment = currentCableSegment + 1;

                    //Rotate
                    transform.eulerAngles = new Vector3(cableplant.points[currentCableSegment].eulerAngles.x, cableplant.points[currentCableSegment].eulerAngles.y + transform.eulerAngles.y, cableplant.points[currentCableSegment].eulerAngles.z);
                }
                else
                {
                    //DisableClimbing();
                    //ApplyFirstJumpForce();
                }
            }
        }
        //S Down
        if (input.Climbing.verticalInput.ReadValue<Vector2>().y < 0)
        {

            if (currentCableSegment - 1 >= 0)
            {
                // Get the direction from the bird's current position to the next cable point
                Vector3 direction = cableplant.points[currentCableSegment - 1].position - transform.position;
                direction.Normalize();

                // Move the bird along the cable in the current segment's direction
                transform.position += direction * speed * Time.deltaTime;
                // Check if the bird has reached the current segment's end point
                if (Vector3.Distance(transform.position, cableplant.points[currentCableSegment - 1].position) < 0.1f)
                {
                    if (currentCableSegment > 1)
                    {
                        // Move to the next segment of the cable
                        currentCableSegment = currentCableSegment - 1;
                        //Rotate
                        transform.eulerAngles = new Vector3(cableplant.points[currentCableSegment].eulerAngles.x, cableplant.points[currentCableSegment].eulerAngles.y + transform.eulerAngles.y, cableplant.points[currentCableSegment].eulerAngles.z);
                    }
                    else
                    {
                        //DisableClimbing();
                        //ApplyFirstJumpForce();
                    }
                }
            }
        }

    }
    public void EnableClimbing()
    {
        // Disable regular movement controls
        rb.isKinematic = true;
        isClimbing = true;
        rb.useGravity = false;
        boxCollider.enabled = false;
        birdBody.transform.localPosition += new Vector3(-0.453f, 0, 0);//Neeeds to be different value or each climbing place make it a 
        birdBody.transform.localEulerAngles += new Vector3(0, 0, 90);
        //animator.SetBool("IsClimbing", true);
        readyToClimb = false;

        rb.velocity = Vector3.zero;
    }

    public void DisableClimbing()
    {
        // Enable regular movement controls

        controllsSwitch.SwitchToFloor();
        rb.isKinematic = false;
        isClimbing = false;
        rb.useGravity = true;
        Invoke("ActivateCollider", 0.2f);
        currentCableSegment = 0;
        atEnd = false;
        birdBody.transform.localPosition = Vector3.zero;
        birdBody.transform.localEulerAngles = Vector3.zero;
        //animator.SetBool("IsClimbing", false);
        Invoke("SetReadyToClimb", 0.2f);
    }


    public void JumpOff(InputAction.CallbackContext input)
    {
        if (isClimbing && input.started)
        {
            DisableClimbing();
            ApplyFirstJumpForce();
        }
    }

    void ApplyFirstJumpForce()
    {

        Vector3 cameraForward = cameraa.transform.forward;
        Vector3 forceDirection = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
        Vector3 force = forceDirection * forcePower;
        rb.AddForce(force);
        Debug.Log("JumpForce");
    }

    void ActivateCollider()
    {
        boxCollider.enabled = true;
    }
    void SetReadyToClimb()
    {
        readyToClimb = true;
    }
}