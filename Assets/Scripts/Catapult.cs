using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    private Animator animator;
    AudioSource aS;
    [SerializeField] AudioClip AudioClip;
    [SerializeField] AudioClip AudioClip1;

    public LoadingCatapult loading;
    public Spoon spoon;
    public IngredientPickup[] ingredients;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        aS = GetComponent<AudioSource>();
        aS.clip = AudioClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && loading.canShoot)
        {
            loading.DisableArrow();
            loading.canShoot = false;
            loading.ResetIngredients();
            spoon.ResetSpoon();
            animator.SetBool("Shoot", true);
            aS.Play();
            Invoke(nameof(NextSound), AudioClip.length - 0.5f);
            foreach (IngredientPickup ingredient in ingredients)
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

    public void NextSound()
    {
        aS.clip = AudioClip1;
        aS.Play();
    }
}
