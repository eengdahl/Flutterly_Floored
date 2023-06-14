using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using Cinemachine;
public class BirdCableMovement : MonoBehaviour
{
    SwitchControls controllsSwitch;
    PlayerControls input = null;
    public ClimbAlongScript cableplant;
    [SerializeField] Camera cameraa;
    [SerializeField] GameObject birdBody;
    Rigidbody rb;
    Collider boxCollider;
    [SerializeField] PlayerJump jumpScript;
    //private InputAction.CallbackContext initialInput;
    [SerializeField] CinemachineVirtualCamera cAmera;
    PlayerMove playerMoveScript;

    [SerializeField]LayerMask groundLayer;

    [Header("Variables")]
    public float forcePower = 20f;
    public float speed = 5f;

    public float isMoving;
    public float horizontalInput;
    public float downSpeed = 5f;
    public float downSpeedMin;
    public float downSpeedMax = 10f;
    public float rotationSpeedAuto = 10f;
    public float rotationSpeed = 10f;
    public bool isVertical;
    public int currentCableSegment = 0;
    public bool readyToClimb;
    public bool isClimbing;
    [SerializeField] float redForce = 10000;
    [SerializeField] float greenForce = 7000;

    //Sinus curve movement speed
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float speedFrequency = 2f;




    StartClimbing startClimbingRef;

    //Raycast for rotation
    bool canTurnRight;
    bool canTurnLeft;
    [SerializeField] GameObject raycastStartOne;
    [SerializeField] GameObject raycastStartTwo;
    //Raycast for up and down

    [SerializeField] GameObject raycastUp;


    [SerializeField] GameObject raycastDown;

    bool inWall;
    //public bool isJungle;


    //Raycast for up or down
    bool canGoUp;
    bool canGoDown;

    //ClimbON rotation
    
    private float rotationSpeedR = 90f;
    private float rotationDuration = 0.6f;

    private Quaternion targetRotationR;
    private bool isRotating = false;
    //ClimbOn position
    public float movementDuration = 1f;
    private bool reachedTargetPosition = false;
    private bool isMovingP = false;
    private bool shouldSetBirdPosition;
    private Coroutine birdPositionCoroutine;

    M2tutorial m2Tutorial; 

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
        m2Tutorial = FindObjectOfType<M2tutorial>();

        downSpeedMin = downSpeed;
        playerMoveScript = GetComponent<PlayerMove>();
        boxCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        controllsSwitch = GetComponent<SwitchControls>();
        readyToClimb = true;
    }

    private void LateUpdate()
    {
        //For rotation
        if (!isClimbing) return;
        if (!isVertical)
        {
            return;
        }
        if (!reachedTargetPosition) return;


        horizontalInput = input.Climbing.verticalInput.ReadValue<Vector2>().x;

        if (horizontalInput > 0 && canTurnRight)
        {

            //Dont do if input is zero, Approximately i think is good when using gamepad
            if (Mathf.Approximately(horizontalInput, 0f))
            {
                return;
            }
            float targetRotation = horizontalInput * rotationSpeed * Time.deltaTime;

            transform.Rotate(0f, targetRotation, 0f);
        }
        else if (horizontalInput < 0 && canTurnLeft)
        {
            //Dont do if input is zero, Approximately i think is good when using gamepad
            if (Mathf.Approximately(horizontalInput, 0f))
            {
                return;
            }
            float targetRotation = horizontalInput * rotationSpeed * Time.deltaTime;

            transform.Rotate(0f, targetRotation, 0f);
        }

    }

    private void FixedUpdate()
    {
        if (!isClimbing) return;
        if (isMovingP) return;
        isMoving = input.Climbing.verticalInput.ReadValue<Vector2>().y;
        if (cableplant.isJungle)
        {
            transform.position = cableplant.points[currentCableSegment].position;
        }

        if (!input.Climbing.LeaveClimbing.IsPressed())
        {
            DisableClimbing();
        }
        //Raycast for up&down
        RaycastHit hitUp;
        if (Physics.Raycast(raycastUp.transform.position, raycastUp.transform.forward, out hitUp, 0.4f,groundLayer))
            canGoUp = false;
        else canGoUp = true;
        RaycastHit hitDown;
        if (Physics.Raycast(raycastDown.transform.position, raycastDown.transform.forward, out hitDown, 0.4f,groundLayer)) 
            canGoDown = false;
        else canGoDown = true;
        Debug.Log(canGoDown);

        Debug.DrawRay(raycastUp.transform.position, raycastUp.transform.forward, Color.red);
        Debug.DrawRay(raycastDown.transform.position, raycastDown.transform.forward, Color.red);

        //Raycast for rotation
        //RaycastHit hitOne;

        //if (Physics.Raycast(raycastStartOne.transform.position, raycastStartOne.transform.forward, out hitOne, 0.1f))
        //{
        //    canTurnLeft = false;
        //}
        //else
        //{
        //    canTurnLeft = true;
        //}
        //RaycastHit hitTwo;
        //if (Physics.Raycast(raycastStartTwo.transform.position, raycastStartTwo.transform.forward, out hitTwo, 0.1f))
        //{
        //    canTurnRight = false;
        //}
        //else
        //{
        //    canTurnRight = true;
        //}

        //if (!canTurnLeft || !canTurnRight)
        //{
        //    inWall = true;
        //}
        //else
        //{
        //    inWall = false;
        //}
        Debug.DrawRay(raycastStartOne.transform.position, raycastStartOne.transform.forward, Color.red);
        Debug.DrawRay(raycastStartTwo.transform.position, raycastStartTwo.transform.forward, Color.red);


        if (cableplant != null)
        {



            Vector3 eulerAngles = cableplant.points[currentCableSegment].eulerAngles;
            if (!isVertical)
            {
                Quaternion targetRotationQ = Quaternion.Euler(eulerAngles);
                // Get the current rotation
                Quaternion currentRotation = transform.rotation;

                // Calculate the rotation to apply towards the target rotation
                Quaternion rotationToApply = Quaternion.RotateTowards(currentRotation, targetRotationQ, 150f * Time.deltaTime);

                // Apply the rotation to the object's transform
                transform.rotation = rotationToApply;

            }


            //W Up
            if (input.Climbing.verticalInput.ReadValue<Vector2>().y > 0 && canGoUp && !cableplant.isJungle)//&& !inWall
            {
                // Get the direction from the bird's current position to the next cable point
                Vector3 direction = cableplant.points[currentCableSegment].position - transform.position;
                direction.Normalize();

                // Calculate the sinus multiplier for the speed
                float timeMultiplier = Mathf.Sin(Time.time * speedFrequency);
                float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, (timeMultiplier + 1f) / 2f);

                // Move the bird along the cable in the current segment's direction with the sinus-based speed
                transform.position += direction * currentSpeed * Time.deltaTime;

                // Check if the bird has reached the current segment's end point
                if (Vector3.Distance(transform.position, cableplant.points[currentCableSegment].position) < 0.1f)
                {
                    if (currentCableSegment < cableplant.points.Count - 1)
                    {
                        // Move to the next segment of the cable
                        currentCableSegment = currentCableSegment + 1;
                        //Rotate
                        if (isVertical)
                        {
                            eulerAngles.y += transform.eulerAngles.y;
                            transform.eulerAngles = eulerAngles;
                        }
                        if (currentCableSegment == 20)
                        {
                            SwitchBranch();
                        }
                    }
                }
            }
            //S Down
            if (input.Climbing.verticalInput.ReadValue<Vector2>().y < 0 && canGoDown && !cableplant.isJungle) //&&!inWall
            {
                if (currentCableSegment - 1 >= 0)
                {
                    // Get the direction from the bird's current position to the next cable point
                    Vector3 direction = cableplant.points[currentCableSegment - 1].position - transform.position;
                    direction.Normalize();

                    // Calculate the sinus multiplier for the speed
                    float timeMultiplier = Mathf.Sin(Time.time * speedFrequency);
                    float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, (timeMultiplier + 1f) / 2f);

                    // Move the bird along the cable in the current segment's direction with the sinus-based speed
                    transform.position += direction * currentSpeed * Time.deltaTime;

                    // Check if the bird has reached the current segment's end point
                    if (Vector3.Distance(transform.position, cableplant.points[currentCableSegment - 1].position) < 0.1f)
                    {
                        if (currentCableSegment > 1)
                        {
                            // Move to the next segment of the cable
                            currentCableSegment = currentCableSegment - 1;
                            // Rotate if necessary
                            if (isVertical)
                            {
                                eulerAngles.y += transform.eulerAngles.y;
                                transform.eulerAngles = eulerAngles;
                            }
                        }
                    }
                }
            }
            //if it is jungle swings
            if (input.Climbing.verticalInput.ReadValue<Vector2>().y > 0 && cableplant.isJungle)
            {
                cableplant.points[currentCableSegment].gameObject.GetComponentInParent<Rigidbody>().AddForce(birdBody.transform.up * 300f);

            }
            if (input.Climbing.verticalInput.ReadValue<Vector2>().y < 0 && cableplant.isJungle)
            {
                cableplant.points[currentCableSegment].gameObject.GetComponentInParent<Rigidbody>().AddForce(-birdBody.transform.up * 300f);

            }
        }
    }


    public void EnableClimbing()
    {
        m2Tutorial.hasDoneClimbing = true;
        
        SetDampeningHigh();
        shouldSetBirdPosition = true;
        // Disable regular movement controls
        ToggleMovement();
        rb.isKinematic = true;
        isClimbing = true;
        rb.useGravity = false;
        if (isVertical)
        {
            boxCollider.enabled = false;
        }
        birdBody.transform.localPosition += new Vector3(-0.453f, 0, 0);//Neeeds to be different value or each climbing place make it a 
        //animator.SetBool("IsClimbing", true);
        readyToClimb = false;
        rb.velocity = Vector3.zero;
        StartCoroutine(RotateBird(90,birdBody.transform));
    }

    public void DisableClimbing()
    {
       // transform.position += new Vector3(0, 0.2f, 0);
        SetDampeningZero();
        //reachedTargetPosition = false;
        playerMoveScript.groundMovement = true;
        // Enable regular movement controls
        controllsSwitch.SwitchToFloor();
        rb.isKinematic = false;
        isClimbing = false;
        rb.useGravity = true;

        if (cableplant != null)
        {
            if (cableplant.canFall)
            {
                cableplant.FallToGround();
            }

            if (cableplant.isJungle)
            {
                rb.velocity = cableplant.points[currentCableSegment].gameObject.GetComponentInParent<Rigidbody>().velocity * 2;
                cableplant = null;
            }
        }
        Invoke("ActivateCollider", 0.6f);
        currentCableSegment = 0;
        transform.localEulerAngles = Vector3.zero;
        birdBody.transform.localPosition = Vector3.zero;
        //Stop setting position
        shouldSetBirdPosition = false;
        if (birdPositionCoroutine != null)
        {
            StopCoroutine(birdPositionCoroutine);
            birdPositionCoroutine = null;
        }
        

        StartCoroutine(RotateBird(0,birdBody.transform));
        Invoke("SetReadyToClimb", 0.6f);

    }
    public void JumpOff(InputAction.CallbackContext input)
    {
        if (isClimbing && input.started)
        {
            DisableClimbing();
            ApplyFirstJumpForce();
        }
    }
    public void ChangeSpeedDown(InputAction.CallbackContext input)
    {
        if (!isVertical) return;
        if (isClimbing && input.started)
        {
            downSpeed = downSpeedMax;
        }
        if (input.canceled)
        {
            downSpeed = downSpeedMin;
        }
    }
    void ApplyFirstJumpForce()
    { 
        Vector3 force = new Vector3(-redForce, greenForce, 0f);
        rb.AddRelativeForce(force);
    }
    void ActivateCollider()
    {
        boxCollider.enabled = true;
    }
    void SetReadyToClimb()
    {
        //ActivateCollider();
        readyToClimb = true;
    }
    public void ToggleMovement()
    {
        playerMoveScript.groundMovement = !playerMoveScript.groundMovement;
    }

    //Get the index and stuff
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ClimbingPart"))
        {
            if (readyToClimb)
            {
                if (input.Floor.Drag.IsPressed())
                {
                    if (!isClimbing)
                    {
                        reachedTargetPosition = false;
                        cableplant = other.gameObject.GetComponent<StartClimbing>().climbAlongScript;
                        currentCableSegment = other.gameObject.GetComponent<StartClimbing>().index;
                        shouldSetBirdPosition = true;
                        birdPositionCoroutine = StartCoroutine(SetBirdPosition(other.transform));
                        //StartCoroutine( SetBirdPosition(other.transform));
                        EnableClimbing();
                        controllsSwitch.SwitchToClimbing();
                        //transform.position = other.gameObject.transform.position;
                        if (cableplant.rotationStartLocked)
                        {
                            transform.rotation = Quaternion.Euler(cableplant.startRotation);
                        }
                        else
                        {
                             transform.rotation = other.transform.rotation;
                            
                        }
                        if (other.gameObject.GetComponent<StartClimbing>().isVertical)
                        {
                            isVertical = true;
                        }
                        else if (!other.gameObject.GetComponent<StartClimbing>().isVertical)
                        {
                            isVertical = false;
                        }

                    }
                }
            }
        }
    }

    private void SwitchBranch()
    {
        if (cableplant.isSmallClimbing)
        {
            currentCableSegment = cableplant.otherClimbingIndex;
            cableplant = cableplant.otherClimbAlong;
            isVertical = false;
        }
         
    }

    private IEnumerator SetBirdPosition(Transform targetGO)
    {

       
        isMovingP = true;

        Transform birdTransform = transform;
        Vector3 initialPosition = birdTransform.position;
        Vector3 targetPosition = targetGO.position;
        float distance = Vector3.Distance(initialPosition, targetPosition);
        float speed = distance / movementDuration;

        float startTime = Time.time;
        while (birdTransform.position != targetPosition)
        {
            float step = speed * Time.unscaledDeltaTime;
            birdTransform.position = Vector3.MoveTowards(birdTransform.position, targetPosition, step);
          
            if (birdTransform.position == targetPosition && !reachedTargetPosition)
            {
                reachedTargetPosition = true;
                
            }
             yield return null;

        }

        isMovingP = false;
    }
    private IEnumerator RotateBird(float degrees, Transform transform)
    {
        isRotating = true;

        Quaternion initialRotation = transform.localRotation;//birdBody.transform.localRotation;
        targetRotationR = Quaternion.Euler( new Vector3(0f, 0f, degrees));

        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            
            birdBody.transform.localRotation = Quaternion.Lerp(initialRotation, targetRotationR, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotationR;
        isRotating = false;
    }

    private void SetDampeningHigh()
    {
        cAmera.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1f;
        cAmera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = 1f;
        cAmera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1f;


    }
    private void SetDampeningZero()
    {
        cAmera.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 0f;
        cAmera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = 0f;
        cAmera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0f;
    }



}