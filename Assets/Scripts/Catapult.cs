using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    private Animator animator;

    public LoadingCatapult loading;
    public Spoon spoon;
    public IngredientPickup[] ingredients;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && loading.canShoot)
        {
            loading.canShoot = false;
            loading.ResetIngredients();
            spoon.ResetSpoon();
            animator.SetBool("Shoot", true);
            foreach(IngredientPickup ingredient in ingredients)
            {
                ingredient.ResetPickups();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Shoot", false);
        }
    }
}
