using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashSoundPicker : MonoBehaviour
{
    AudioSource aS;
    [SerializeField] AudioClip[] sounds;
    int indexPicker;
    float pitch;
    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        pitch = Random.Range(0.9f, 1.1f);
    }
    void Start()
    {
        aS.pitch = pitch;
        indexPicker = Random.Range(0, sounds.Length);
        aS.clip = sounds[indexPicker];
        aS.Play();


    }


}
