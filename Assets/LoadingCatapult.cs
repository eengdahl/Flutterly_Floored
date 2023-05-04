using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCatapult : MonoBehaviour
{
    public bool isSugarFilled, isFlourFilled, isEggFilled;
    public bool canShoot;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && other.GetComponentInParent<Spoon>().isFull)
        {
            Debug.Log(other.gameObject.name);
            Spoon spoon = other.GetComponentInParent<Spoon>();

            switch (spoon.currentIngredient)
            {
                case "Flour":
                    {
                        isFlourFilled = true;
                        spoon.flourFilled = true;
                        break;
                    }
                case "Sugar":
                    {
                        isSugarFilled = true;
                        spoon.sugarFilled = true;
                        break;
                    }
                case "Egg":
                    {
                        isEggFilled = true;
                        spoon.eggFilled= true;
                        break;
                    }
            }

            spoon.isFull = false;

            if (isFlourFilled && isSugarFilled && isEggFilled)
            {
                canShoot = true;
            }
        }
    }

    public void ResetIngredients()
    {
        isFlourFilled = false;
        isEggFilled = false;
        isSugarFilled = false;
    }

}
