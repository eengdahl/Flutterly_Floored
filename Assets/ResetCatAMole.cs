using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCatAMole : MonoBehaviour
{
    [SerializeField] GameObject spoon;
    Vector3 spoonPosition;
    Quaternion spoonRotation;
    [SerializeField]Spoon spoonScript;
    [SerializeField]IngredientPickup[] pickUps;

    private void Start()
    {
        
        spoonRotation = spoon.transform.rotation;
        spoonPosition = spoon.transform.position;
    }
    public void ResetSpoon()
    {
        spoon.transform.position = spoonPosition;
        spoon.transform.rotation = spoonRotation;
        spoonScript.isFull = false;
        EmptySpoon();
        spoonScript.EmptySpoon(true);
    }

    private void EmptySpoon()
    {
        if (spoonScript.sugarInSpoon)
        {

            foreach (IngredientPickup adad in pickUps)
            {
                if (adad.isSugar)
                {
                    adad.hasBeenPickedUp = false;
                    adad.ReturnGlow();
                }
            }
            spoonScript.sugarInSpoon = false;
        }
        else if (spoonScript.milkInSpoon)
        {

            foreach (IngredientPickup adad in pickUps)
            {
                if (adad.isMilk)
                {
                    adad.hasBeenPickedUp = false;
                    adad.ReturnGlow();
                }
            }
            spoonScript.milkInSpoon = false;
        }
        else if (spoonScript.flourInSpoon)
        {

            foreach (IngredientPickup adad in pickUps)
            {
                if (adad.isFlour)
                {
                    adad.hasBeenPickedUp = false;
                    adad.ReturnGlow();
                }
            }
            spoonScript.flourInSpoon = false;
        }
    }
}
