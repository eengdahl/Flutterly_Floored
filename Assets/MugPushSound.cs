using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugPushSound : MonoBehaviour
{
    AudioSource aS;
    [SerializeField]AudioClip[] pushSounds;
    int pushIndex;
    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pushIndex = Random.Range(0, pushSounds.Length);
            aS.clip = pushSounds[pushIndex]; 
            aS.Play();
        }
    }
}
