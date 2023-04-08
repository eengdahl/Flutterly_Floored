using UnityEngine;
using UnityEngine.InputSystem;

public class RotateOnLadder : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private InputAction horizontalInput;

    private void OnEnable()
    {
        horizontalInput = new InputAction("Horizontal", InputActionType.Value, "<Keyboard>/a", "<Keyboard>/d");
        horizontalInput.Enable();
        horizontalInput.performed += OnHorizontalInputPerformed;
    }

    private void OnDisable()
    {
        horizontalInput.Disable();
        horizontalInput.performed -= OnHorizontalInputPerformed;
    }

    private void OnHorizontalInputPerformed(InputAction.CallbackContext obj)
    {
        float horizontalValue = obj.ReadValue<float>();
        float targetRotation = horizontalValue * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, targetRotation, 0f);
    }
}
