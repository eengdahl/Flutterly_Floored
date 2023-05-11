using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LoadingCatapult : MonoBehaviour
{
    public bool isSugarFilled, isFlourFilled, isMilkFilled;
    public float ingredientsNeeded; 
    public bool canShoot;
    public int ingredientCount;
    public GameObject cat;
    public GameObject hitspots;
    public GameObject scaleableObject;

    private Vector3 startScale;
    private Vector3 sizeToScale;


    private void Start()
    {
        startScale = Vector3.one;
        sizeToScale = startScale / ingredientsNeeded;
        scaleableObject.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && other.GetComponentInParent<Spoon>().isFull)
        {
            Debug.Log(other.gameObject.name);
            Spoon spoon = other.GetComponentInParent<Spoon>();

            switch (spoon.currentIngredient)
            {
                case "FlourPile":
                    {
                        isFlourFilled = true;
                        spoon.flourFilled = true;
                        ingredientCount++;
                        ScaleUpMoshyThing();
                        break;
                    }
                case "SugarPile":
                    {
                        isSugarFilled = true;
                        spoon.sugarFilled = true;
                        ingredientCount++;
                        ScaleUpMoshyThing();
                        break;
                    }
                case "MilkPile":
                    {
                        isMilkFilled = true;
                        spoon.milkFilled = true;
                        ingredientCount++;
                        ScaleUpMoshyThing();
                        break;
                    }
            }

            //spoon.isFull = false;
            spoon.EmptySpoon();

            if (isFlourFilled && isSugarFilled && isMilkFilled)
            {
                canShoot = true;
            }
        }
    }

    private void ScaleUpMoshyThing()
    {
        scaleableObject.transform.localScale += sizeToScale;
    }

    public void ResetIngredients()
    {
        scaleableObject.transform.localScale = Vector3.zero;
        ingredientCount = 0;
        isFlourFilled = false;
        isMilkFilled = false;
        isSugarFilled = false;
        //Invoke(nameof(DespawnCat), 2);
       // StartCoroutine(DespawnCat());

    }

    private void DespawnCat()
    {
       // yield return new WaitForSeconds(1.5f);
        cat.SetActive(false);
        hitspots.SetActive(false);
    }

}
