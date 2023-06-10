using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.VisualScripting;
using System.Security.Claims;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float airControl;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] GameObject stepUpBottom;
    [SerializeField] GameObject stepUpTop;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 0.2f;

    private float standardVolume;
    private bool sprintLock;
    private bool flapLock;
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
    SteppScript steppScript;
    public CinemachineVirtualCamera virtualCamera;
    PlayerWind playerWindScrips;
    private float fovFloat;
    //Audio
    public AudioClip sprint;
   public AudioClip flySound;
   // public AudioClip sineglFlap;
    private AudioSource aS;

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
    private string Walk = "Hop 4";
    private string Sprint = "Sprint test 1"; //Wrong Animation
    private string Glide = "Glide flap";
    private string Fall = "Fall";
    private string Impact = "Impact";
    private string Jump = "Jump";
    private string JumpLand = "Jump land";
    private string ClimbLeft = "Climb left";
    private string ClimbRight = "Climb right 3";
    private string activeString;
    private string stringToPlay;
    private string rightIdle = "Climb right 3 idle";
    private string leftIdle = "Climb left idle";
    private string flight = "Flight sustain";
    private string peck = "Peck";
    private string glideFail = "Glide fail extreme";

    private void Awake()
    {
        //stepUpTop.transform.position = new Vector3(stepUpTop.transform.position.x, stepHeight, stepUpTop.transform.position.z);
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
        standardVolume = aS.volume;
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
            stringToPlay = flight;
            if (!flapLock)
            {
                aS.volume = 2;
              //  aS.PlayOneShot(flySound);

                flapLock = true;
            }
        }

        if (jump.canCrash && activeString == Fall && rb.velocity.y <= 0)
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
                animator.speed = 1;
                if (stringToPlay == ClimbLeft)
                {
                    stringToPlay = leftIdle;
                }
                else if (stringToPlay == ClimbRight)
                {
                    stringToPlay = rightIdle;
                }
            }
        }
        if (activeString != stringToPlay)
        {
            aS.Stop();
            aS.volume = standardVolume;
            flapLock = false;
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
        UpdateTargetDirection();

        //stepClimb();
        virtualCamera.m_Lens.FieldOfView = fovFloat;

        if (inputsXZ == Vector3.zero && !jump.hasCanceledGlide && !jump.gliding && !jump.canCrash && !climb.isClimbing && jump.isGrounded)
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
                dustPS.SetActive(true);
                stringToPlay = Sprint;

            }
            else if (maxSpeed == 3 && jump.isGrounded)
            {
                fovFloat = Mathf.MoveTowards(fovFloat, 60, 5 * Time.deltaTime);
                stringToPlay = Walk;
                dustPS.SetActive(true);

            }
            rb.AddForce(targetDirection * speed * 10f, ForceMode.Force);
        }
        else
        {
            dustPS.SetActive(false);


        }

        //in air
        if (!jump.isGrounded)
        {
            //if (!jump.gliding && rb.velocity.y < -5)
            //{
            //    //stringToPlay = glideFail;
            //}
            //if (jump.hasCanceledGlide && rb.velocity.y < -5)
            //{
            //    //stringToPlay = glideFail;
            //}

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



        if (inputsXZ != Vector3.zero && targetDirection.magnitude > 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, targetDirection, turnSpeed * turnSpeedMultiplier * Time.deltaTime);
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
        var lookDirection = transform.position - new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        var forward = lookDirection.normalized;
        forward.y = 0;
        //get the right-facing direction of the referenceTransform
        var right = new Vector3(mainCamera.transform.right.x, 0, mainCamera.transform.right.z);

        // determine the direction the player will face based on input and the referenceTransform's right and forward directions
        targetDirection = inputsXZ.x * right + inputsXZ.z * forward;
    }

    void stepClimb()
    {
        RaycastHit hitBottom;
        Debug.DrawLine(stepUpBottom.transform.position, stepUpBottom.transform.position + new Vector3(0, 0, 0.3f), Color.green);
        if (Physics.Raycast(stepUpBottom.transform.position, stepUpBottom.transform.TransformDirection(Vector3.forward), out hitBottom, 0.2f))
        {
            RaycastHit hitTop;
            if (!Physics.Raycast(stepUpTop.transform.position, stepUpTop.transform.TransformDirection(Vector3.forward), out hitTop, 0.3f))
            {
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }
}