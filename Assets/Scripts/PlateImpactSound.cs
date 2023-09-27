using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateImpactSound : MonoBehaviour
{

    [SerializeField]AudioClip[] arrayOfImpactSounds;
    AudioSource aS;
    int soundIndex;
    AudioClip aC;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag ("Player"))
        {
            soundIndex = Random.Range(0, arrayOfImpactSounds.Length);
            aC = arrayOfImpactSounds[soundIndex];
            aS.clip = aC;
            aS.Play();
        }
    }
}
