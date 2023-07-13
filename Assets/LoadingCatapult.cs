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



    //Materials for glow
    [SerializeField]Renderer rend;
    private Material originalMaterial;
    [SerializeField] Material glowMaterial;

    //Sounds for empty mått
    [SerializeField] AudioClip emptySound;
    AudioSource aS;

    [SerializeField] GameObject arrowForJumpSpot;

    private void Start()
    {
        originalMaterial = rend.material;
        startScale = Vector3.one;
        sizeToScale = startScale / ingredientsNeeded;
        scaleableObject.transform.localScale = Vector3.zero;
        aS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && other.GetComponentInParent<Spoon>().isFull)
        {
            //Debug.Log(other.gameObject.name);
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
            //ljud för att tömma spoon
            aS.clip = emptySound;
            aS.Play();
            if (isFlourFilled && isSugarFilled && isMilkFilled)
            {
                canShoot = true;
                EnableArrow();
            }
        }
    }

    private void ScaleUpMoshyThing()
    {
        scaleableObject.transform.localScale += sizeToScale;
        LightOnMoshyThing();
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
    private void LightOnMoshyThing()
    {
        rend.material = glowMaterial;
        Invoke(nameof(ReturnNormalOnMoshyThing), 2f);
    }
    private void ReturnNormalOnMoshyThing()
    {
        rend.material = originalMaterial;
    }
    private void EnableArrow()
    {
        arrowForJumpSpot.SetActive(true);
    }
    public void DisableArrow()
    {
        arrowForJumpSpot.SetActive(false);
    }

}
