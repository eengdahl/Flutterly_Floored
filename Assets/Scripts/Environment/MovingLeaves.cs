using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeaves : MonoBehaviour
{
    public Animator anim;

    private bool canPlayiAnimation;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canPlayiAnimation = true;
        anim.Play("LeafIdleSway", -1, Random.Range(0.0f, 1.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canPlayiAnimation)
        {
            anim.SetTrigger("Flutter");
            canPlayiAnimation = false;
            StartCoroutine(CooldownTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && canPlayiAnimation)
        {
            anim.SetTrigger("Flutter");
            canPlayiAnimation = false;
            StartCoroutine(CooldownTimer());

        }
    }

    private IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(2f);
        canPlayiAnimation = true;
    }
}
