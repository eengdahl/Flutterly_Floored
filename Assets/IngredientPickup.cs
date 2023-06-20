using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    Spoon spoonScript;
    public bool hasBeenPickedUp;
    public bool isMilk, isSugar, isFlour;

    //For colour setting
    [SerializeField] GameObject glowingObject;
    [SerializeField] Material glowMaterial;
    Material originalMaterial;
    Renderer glowingRenderer;


    private void Start()
    {

        //Get original material and set glow
        glowingRenderer = glowingObject.GetComponent<Renderer>();
        originalMaterial = glowingRenderer.material;
        glowingRenderer.material = glowMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && hasBeenPickedUp == false)
        {
            if (!other.GetComponentInParent<Spoon>().isFull)
            {
                
                    spoonScript = other.GetComponentInParent<Spoon>();
                    Spoon spoon = other.GetComponentInParent<Spoon>();
                    //spoon.isFull = true;
                    spoon.FillSpoon();
                    spoon.currentIngredient = gameObject.name;
                    hasBeenPickedUp = true;
                    SetSpoonFilling();
                    RemoveGlow();
                    
                
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

    void RemoveGlow()
    {
        glowingRenderer.material = originalMaterial;
    }
    public void ReturnGlow()
    {
        glowingRenderer.material = glowMaterial;
    }
}