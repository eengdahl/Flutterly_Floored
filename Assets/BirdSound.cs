using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    public List<AudioClip> clipList;
    AudioSource aS;
    float randomTimer;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        randomTimer = 20;
        Invoke(nameof(MakeRandomNoise), randomTimer);
    }
    private void MakeRandomNoise()
    {
        aS.clip = clipList[Random.Range(0, clipList.Count - 1)];
        aS.Play();
        randomTimer = Random.Range(5, 20);
        Invoke(nameof(MakeRandomNoise), randomTimer);
    }
}
