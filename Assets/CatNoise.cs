using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatNoise : MonoBehaviour
{

    public List<AudioClip> clipList;
    AudioSource aS;
    float randomTimer;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        randomTimer = 2;
        Invoke(nameof(MakeRandomNoise), randomTimer);
    }
    private void MakeRandomNoise()
    {
        aS.clip = clipList[Random.Range(0, clipList.Count-1)];
        aS.Play();
        randomTimer = Random.Range(5, 8);
        Invoke(nameof(MakeRandomNoise), randomTimer);
    }
}
