using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour
{
    public List<AudioClip> horribleDeathSounds;
    private bool locker = true;
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

            if (gameObject.CompareTag("WaterDeathCube") && locker == true)
            {
                clipToPlay = horribleDeathSounds[1];
                Buoyancy[] buoyancyObjects = gameObject.transform.parent.GetComponentsInChildren<Buoyancy>();
                audioSource.PlayOneShot(clipToPlay);
                locker = false;
                foreach(Buoyancy obj in buoyancyObjects)
                {
                    StartCoroutine(DelayPlateReset(obj));
                }
            }
            Invoke(nameof(Unlock), 2);
            playerDeath = other.GetComponent<DeathScriptAndCheckPoint>();
            other.GetComponent<Rigidbody>().isKinematic = false;
            playerDeath.Die();
        }

        IEnumerator DelayPlateReset(Buoyancy obj)
        {
            yield return new WaitForSeconds(2);
            obj.ResetPosition();
        }
    }

    void Unlock()
    {
        locker = true;
    }
}
