using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClimbOld : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public bool isClimbing = false;
    [SerializeField] float climbSpeed;
    PlayerControls input = null;


    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();

    }


    void Awake()
    {
        input = new PlayerControls();
       // input = GetComponent<PlayerControls>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void EnableClimbing()
    {
        Debug.Log("Enabled");
        // Disable regular movement controls
        isClimbing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        //animator.SetBool("IsClimbing", true);

    }

    public void DisableClimbing()
    {
        // Enable regular movement controls
        isClimbing = false;
        rb.useGravity = true;
        //animator.SetBool("IsClimbing", false);
    }

    private void Update()
    {
        if (isClimbing && Input.GetKey(KeyCode.Space))
        {
                        
        }
    }
    void FixedUpdate()
    {
        if (!isClimbing) return;
        HandleClimbingMovement(input.Climbing.verticalInput.ReadValue<Vector2>());
    }
    public void HandleClimbingMovement(Vector2 verticalInput)
    {
        Vector3 moveDirection = new Vector3(verticalInput.x, verticalInput.y, 0f).normalized;
        rb.velocity = moveDirection * climbSpeed;

    }
    public void JumpOff(InputAction.CallbackContext input)
    {
        rb.AddForce(transform.up * 100000f);
    }
}

