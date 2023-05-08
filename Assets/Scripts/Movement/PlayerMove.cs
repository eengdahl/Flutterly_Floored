using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.VisualScripting;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float airControl;
    [SerializeField] float turnSpeed = 10f;

    private float turnSpeedMultiplier;
    private float lerpDuration = 3;
    private Rigidbody rb;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private Vector2 mouseDelta;
    private Vector3 inputsXZ;
    private Vector3 targetDirection;
    private Quaternion freeRotation;
    private Camera mainCamera;
    private Vector3 normalizedVel;
    private AudioSource aS;
    SteppScript steppScript;
    public CinemachineVirtualCamera virtualCamera;
    private float fovFloat;

    PlayerWind playerWindScrips;

    public bool animationLock;
    public bool groundMovement;

    //public Transform MainCamera;

    private PlayerJump jump;
    private BirdCableMovement climb;

    public GameObject dustPS;
    public Animator animator;
    //Animation ShortCuts
    //private static readonly string Idle = Animator.StringToHash("Idle sustainCopy");
    //private static readonly string Walk = Animator.StringToHash("Sprint test 1Copy");
    private string Idle = "Idle sustain";
    private string Walk = "Hop_Copy";
    private string Sprint = "Sprint test 1"; //Wrong Animation
    private string Glide = "Glide flap";
    private string Fall = "Fall";
    private string Impact = "Impact";
    private string Jump = "Jump";
    private string ClimbLeft = "Climb left";
    private string ClimbRight = "Climb right 3";
    private string activeString;
    private string stringToPlay;


    private void Awake()
    {
        steppScript = FindObjectOfType<SteppScript>();
        fovFloat = 60f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        groundMovement = true;
        mainCamera = Camera.main;
        playerWindScrips = GetComponent<PlayerWind>();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<PlayerJump>();
        climb = GetComponent<BirdCableMovement>();
        animator.CrossFade(Idle, 0);
        aS = gameObject.GetComponent<AudioSource>();
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
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
        MovementCommunicator.instance.NotifyLookListeners(mouseDelta.x);
    }
    private void Update()
    {

        //hold shift to sprint
        if (Input.GetKey(KeyCode.LeftShift) && jump.isGrounded)
        {
            maxSpeed = 5;
        }
        //if not holding sprint on ground, walk
        if (!Input.GetKey(KeyCode.LeftShift) && jump.isGrounded)
        {
            maxSpeed = 3;

        }
        //if releasing sprint key on ground start walking
        if (Input.GetKeyUp(KeyCode.LeftShift) && jump.isGrounded)
        {
            maxSpeed = 3;
        }
        if (maxSpeed > 3 && !jump.isGrounded)
        {
            maxSpeed = Mathf.Lerp(5, 3, jump.airTime / lerpDuration);
        }

        if (jump.gliding)
        {
            stringToPlay = Glide;
        }

        if (jump.canCrash && rb.velocity.y == 0)
        {
            stringToPlay = Impact;
        }

        if (!jump.isGrounded && !jump.gliding && rb.velocity.y > 0)
        {
            stringToPlay = Jump;
        }

        if (climb.isClimbing)
        {
            if (climb.isMoving > 0)
            {
                animator.speed = 1;
                stringToPlay = ClimbRight;
            }
            if (climb.isMoving < 0)
            {
                animator.speed = 1;
                stringToPlay = ClimbLeft;
            }
            else if (climb.isMoving == 0)
            {
                animator.speed = 0;
            }
        }
        if (activeString != stringToPlay)
        {
            steppScript.StopStepSound();
            if (stringToPlay == Idle)
            {
                steppScript.StopStepSound();
                AnimationWithDelay(stringToPlay, 0.1f);
                activeString = stringToPlay;
                return;
            }
            activeString = stringToPlay;
            AnimationWithDelay(activeString);
        }
    }

    private void AnimationWithDelay(string animString, float loadTime = 0.5f)
    {
        if (animator.speed == 0)
        {
            animator.speed = 1;

        }
        animator.CrossFade(animString, loadTime);
    }

    private void FixedUpdate()
    {

        virtualCamera.m_Lens.FieldOfView = fovFloat;

        if (inputsXZ == Vector3.zero && !jump.hasCanceledGlide && !jump.gliding && !jump.canCrash && !climb.isClimbing)
        {
            stringToPlay = Idle;
            fovFloat = Mathf.MoveTowards(fovFloat, 60, 10 * Time.deltaTime);
        }


        if (!groundMovement)
        {
            return;
        }
        SpeedControl();
        normalizedVel = rb.velocity;
        normalizedVel.y = 0;
        if (jump.isGrounded && targetDirection.magnitude > 0.1f)
        {

            if (maxSpeed == 5 && jump.isGrounded)
            {
                fovFloat = Mathf.MoveTowards(fovFloat, 50, 5 * Time.deltaTime);
                stringToPlay = Sprint;
                dustPS.SetActive(true);
                aS.enabled = true;
            }
            else if (maxSpeed == 3 && jump.isGrounded)
            {
                fovFloat = Mathf.MoveTowards(fovFloat, 60, 5 * Time.deltaTime);
                stringToPlay = Walk;
                dustPS.SetActive(true);
                aS.enabled = true;
            }
            rb.AddForce(targetDirection * speed * 10f, ForceMode.Force);
        }
        else
        {
            dustPS.SetActive(false);
            aS.enabled = false;

        }

        //in air
        if (!jump.isGrounded)
        {
            if (!jump.gliding && rb.velocity.y < -5)
            {
                stringToPlay = Fall;
            }
            if (jump.hasCanceledGlide && rb.velocity.y < -5)
            {
                stringToPlay = Fall;
            }

            if (!playerWindScrips.inWindZone)
            {
                rb.AddForce(10f * airControl * speed * targetDirection, ForceMode.Force);

            }
            else
            {
                rb.AddForce(10f * airControl * speed * new Vector3(inputsXZ.x, 0f, 0f), ForceMode.Force);
            }
        }

        Vector3 moveDirection = transform.forward.normalized * moveInput.y + (Camera.main.transform.right.normalized * moveInput.x);
        inputsXZ = new Vector3(moveInput.x, 0f, moveInput.y);
        //+(Camera.main.transform.right.normalized * moveInput.x)


        UpdateTargetDirection();

        if (inputsXZ != Vector3.zero && targetDirection.magnitude > 0.1f)
        {


            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
        }
        //on ground
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limits speed on ground
        if (jump.isGrounded && flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        //limits speed in air
        else if (!jump.isGrounded && flatVel.magnitude > (maxSpeed * airControl))
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    public virtual void UpdateTargetDirection()
    {
        turnSpeedMultiplier = 1f;
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        //get the right-facing direction of the referenceTransform
        var right = mainCamera.transform.TransformDirection(Vector3.right);

        // determine the direction the player will face based on input and the referenceTransform's right and forward directions
        targetDirection = inputsXZ.x * right + inputsXZ.z * forward;
    }
}