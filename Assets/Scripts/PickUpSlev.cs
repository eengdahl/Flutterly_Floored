using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSlev : MonoBehaviour
{
    public Transform holdPoint; // The point on the bird's mouth where the item will be held
    private GameObject heldItem; // The item currently being held by the bird
    private Rigidbody itemRb; // The rigidbody component of the held item
    //Vector3 holdRotation; // New variable for the rotation of the hold point
    [SerializeField] DisplayM2Spoon slevPickUpTutorial;
    [SerializeField] IngredientPickup[] ingredientPickups;
    [SerializeField] GameObject pickupArrow;
    [SerializeField] LoadingCatapult loading;

    void Start()
    {
        //holdPoint.rotation = holdRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slev"))
        {
            if (heldItem == null)
            {

                heldItem = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slev"))
        {

            heldItem = null;

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
                    itemRb.isKinematic = true; // Disable physics on held item
                }
                ActivateArrows();
                // Set the position and rotation of the held item to the hold point position and rotation
                heldItem.transform.SetParent(holdPoint);
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.transform.rotation = holdPoint.rotation;
                if (slevPickUpTutorial.show == true)
                {
                    ActivateGlow();
                    slevPickUpTutorial.show = false;
                    slevPickUpTutorial.DeactivateM2();
                }
                pickupArrow.SetActive(false);
            }
        }

        if (context.canceled)
        {
            OnDrop();
        }
    }

    public void OnDrop()
    {
        //Drop the item
        if (heldItem != null)
        {
            heldItem.transform.parent = null;
            itemRb = heldItem.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                itemRb.isKinematic = false; // Enable physics on the dropped item
            }
            DeactivateArrows();
            if (!loading.canShoot)
            {
                pickupArrow.SetActive(true);

            }
            heldItem = null;
        }

    }

    private void ActivateGlow()
    {
        foreach (IngredientPickup ingredient in ingredientPickups)
        {
            ingredient.ReturnGlow();
        }
    }
    public void ActivateArrows()
    {
        foreach (IngredientPickup ingredient in ingredientPickups)
        {
            if (!ingredient.hasBeenPickedUp)
            {
                ingredient.ActivateArrow();
            }
        }
    }
    public void DeactivateArrows()
    {
        foreach (IngredientPickup ingredient in ingredientPickups)
        {

            ingredient.DisableArrow();

        }
    }
}