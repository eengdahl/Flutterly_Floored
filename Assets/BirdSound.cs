using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    public List<AudioClip> clipList;
    public Animator animator;
    float randomTimer;
    [SerializeField] AudioSource aS;
    string openBeak = "OpenBeak";

    private void Start()
    {

        aS = GetComponent<AudioSource>();
        randomTimer = 2;
        Invoke(nameof(MakeRandomNoise), randomTimer);
    }
    private void MakeRandomNoise()
    {
        aS.clip = clipList[Random.Range(0, clipList.Count - 1)];
        aS.Play();
        randomTimer = Random.Range(5, 20);
        animator.CrossFade(openBeak, 0, 1);
        Invoke(nameof(MakeRandomNoise), randomTimer);
    }

}
