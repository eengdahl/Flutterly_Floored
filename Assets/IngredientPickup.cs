using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    Spoon spoonScript;
    public bool hasBeenPickedUp;
    public bool isMilk, isSugar, isFlour;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && hasBeenPickedUp == false)
            if (!other.GetComponentInParent<Spoon>().isFull)
            {
                {
                    spoonScript = other.GetComponentInParent<Spoon>();
                    Spoon spoon = other.GetComponentInParent<Spoon>();
                    //spoon.isFull = true;
                    spoon.FillSpoon();
                    spoon.currentIngredient = gameObject.name;
                    hasBeenPickedUp = true;
                    SetSpoonFilling();
                }
            }
    }

    public void ResetPickups()
    {

        hasBeenPickedUp = false;
    }

    void SetSpoonFilling()
    {
        if (isMilk)
        {
            spoonScript.milkInSpoon = true;
        }
        else if (isSugar)
        {
            spoonScript.sugarInSpoon = true;
        }
        else if (isFlour)
        {
            spoonScript.flourInSpoon = true;
        }
    }
    private void ResetOnePickUp()
    {

    }


}