using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnClick : MonoBehaviour
{
    AudioSource aS;
    [SerializeField] AudioClip aClip;
    // Start is called before the first frame update
    void Awake()
    {
        aS = GetComponent<AudioSource>();
        aS.clip = aClip;
    }

    public void OnClick()
    {
        aS.Play();
    }
}
