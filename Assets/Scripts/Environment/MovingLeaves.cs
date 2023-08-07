using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeaves : MonoBehaviour
{
    public Animator anim;

    private bool canPlayiAnimation;
    [SerializeField] AudioClip[] bounceSounds;
    AudioClip bounceSound;
    AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        canPlayiAnimation = true;
        anim.Play("LeafIdleSway", -1, Random.Range(0.0f, 1.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canPlayiAnimation|| other.gameObject.CompareTag("Ground") && canPlayiAnimation)
        {
            if (canPlayiAnimation)
            {
                int soundsIndexPicker = Random.Range(0, bounceSounds.Length);
                bounceSound = bounceSounds[soundsIndexPicker];
                aS.clip = bounceSound;
                aS.Play();
            }
            anim.SetTrigger("Flutter");
            canPlayiAnimation = false;
            StartCoroutine(CooldownTimer());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && canPlayiAnimation||other.gameObject.CompareTag("Ground") && canPlayiAnimation)
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
