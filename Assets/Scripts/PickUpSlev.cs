using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSlev : MonoBehaviour
{
    public Transform holdPoint; // The point on the bird's mouth where the item will be held
    private GameObject heldItem; // The item currently being held by the bird
    private Rigidbody itemRb; // The rigidbody component of the held item
    public Quaternion holdRotation; // New variable for the rotation of the hold point
    void Start()
    {
        holdPoint.rotation = holdRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slev"))
        {
            heldItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slev"))
        {

            // heldItem = null;

        }
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (heldItem != null)
            {
                itemRb = heldItem.GetComponent<Rigidbody>();
                if (itemRb != null)
                {
                    itemRb.isKinematic = true; // Disable physics on the held item
                }

                Vector3 holdPointWorldPosition = holdPoint.parent.TransformPoint(holdPoint.localPosition);
                heldItem.transform.position = holdPointWorldPosition;

                Vector3 holdPointWorldDirection = holdPoint.parent.TransformDirection(holdPoint.localRotation * Vector3.forward);
                Quaternion heldItemRotation = Quaternion.LookRotation(holdPointWorldDirection, Vector3.up);
                heldItem.transform.rotation = heldItemRotation;

                heldItem.transform.SetParent(holdPoint);
            }
        }

        if (context.canceled)
        {
            OnDrop();
        }
    }

    public void OnDrop()
    {

        if (heldItem != null)
        {
            // If the bird is holding an item, drop it
            heldItem.transform.parent = null;
            itemRb = heldItem.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                itemRb.isKinematic = false; // Enable physics on the dropped item
            }
            heldItem = null;
        }

    }
}