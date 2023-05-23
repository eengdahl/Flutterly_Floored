using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashSoundPicker : MonoBehaviour
{
    AudioSource aS;
    [SerializeField] AudioClip[] sounds;
    int indexPicker;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }
    void Start()
    {
        indexPicker = Random.Range(0, sounds.Length);
        aS.clip = sounds[indexPicker];
        aS.Play();


    }


}
