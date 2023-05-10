using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    public bool hasBeenPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && hasBeenPickedUp == false)
            if (!other.GetComponentInParent<Spoon>().isFull)
            {
                {
                    Spoon spoon = other.GetComponentInParent<Spoon>();
                    //spoon.isFull = true;
                    spoon.FillSpoon();
                    spoon.currentIngredient = gameObject.name;
                    hasBeenPickedUp = true;
                }
            }
    }

    public void ResetPickups()
    {
        hasBeenPickedUp = false;
    }
}
