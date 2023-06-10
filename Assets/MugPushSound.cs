using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugPushSound : MonoBehaviour
{
    AudioSource aS;
    [SerializeField]AudioClip[] pushSounds;
    int pushIndex;
    bool canMakeSound;
    private void Start()
    {
        canMakeSound = false;
        aS = GetComponent<AudioSource>();
        Invoke(nameof(CanMakeSound),2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||other.CompareTag("Ground")&&canMakeSound)
        {
            pushIndex = Random.Range(0, pushSounds.Length);
            aS.clip = pushSounds[pushIndex]; 
            aS.Play();
        }
    }

    void CanMakeSound()
    {
        canMakeSound = true;
    }
}
