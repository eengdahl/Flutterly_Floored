using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour
{
    public List<AudioClip> horribleDeathSounds;

    private AudioSource audioSource;
    private AudioClip clipToPlay;


    DeathScriptAndCheckPoint playerDeath;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.CompareTag("PawDeathCube"))
            {
                clipToPlay = horribleDeathSounds[0];
                audioSource.PlayOneShot(clipToPlay);

                Debug.Log("Input CatKillingPlayerSound here");
            }

            playerDeath = other.GetComponent<DeathScriptAndCheckPoint>();
            other.GetComponent<Rigidbody>().isKinematic = false;
            playerDeath.Die();
        }
    }
}
