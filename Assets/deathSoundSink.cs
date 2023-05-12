using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSoundSink : MonoBehaviour
{
    AudioSource aS;
    public AudioClip birdDie;
    bool locker;
    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (locker)
            {
                aS.Play();
                locker = false;
                Invoke(nameof(ResetBool), 2);
            }
        }
    }

    void ResetBool()
    {
        locker = true;
    }
}
