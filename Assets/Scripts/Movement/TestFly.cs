using UnityEngine;
using UnityEngine.InputSystem;

public class TestFly : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float glideDrag = 0.5f;
    public float diveDrag = 0.2f;
    public float rotationSpeed;
    
    private PlayerControls input = null;
    private Rigidbody rb;
    private float speed;


    bool flyMovementActive;
    private void Awake()
    {
        flyMovementActive = false;
        input = new PlayerControls();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void FixedUpdate()
    {

        if (!flyMovementActive) return;
        // Apply drag based on current gliding angle
        float glideAngle = Vector3.Angle(rb.velocity, transform.forward);
        float glideDragMultiplier = Mathf.Lerp(1f, glideDrag, glideAngle / 180f);
        float diveDragMultiplier = Mathf.Lerp(1f, diveDrag, glideAngle / 180f);
        float dragMultiplier = Mathf.Lerp(glideDragMultiplier, diveDragMultiplier, input.WindZoneMovement.Dive.ReadValue<float>());

        // Apply forces to the glider
        Vector3 moveForce = transform.forward * speed * dragMultiplier;
        rb.AddForce(moveForce, ForceMode.Acceleration);

        //// Rotate the glider based on input
        //Vector2 moveInput = input.WindZoneMovement.Move.ReadValue<Vector2>();
        //Vector3 rotation = new Vector3(moveInput.y, moveInput.x, 0f) * rotationSpeed;
        //transform.Rotate(rotation);

        // Calculate acceleration based on input
        float accelerationInput = 1;//input.WindZoneMovement.Glide.ReadValue<float>() - input.WindZoneMovement.Dive.ReadValue<float>();
        speed = Mathf.Clamp(speed + accelerationInput * acceleration, 0f, maxSpeed);

        // Set glider velocity to match speed and angle
        rb.velocity = transform.forward * speed;
    }

    public void InvertFly()
    {
        flyMovementActive = !flyMovementActive;
    }
    
}