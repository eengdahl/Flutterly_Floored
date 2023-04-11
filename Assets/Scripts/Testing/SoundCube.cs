using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;

public class SoundCube : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip musicClip;

    [Range(0, 1)]
    public float soundVolume = 0.5f;
    public string soundTrigger;
    public float updateTime = 0.01f;
    public float minDeltaSize = -0.1f;
    public float maxDeltaSize = 0.3f;
    public int numberOfSamples = 1024; //Determines volume

    
    private Vector3 defaultScale;
    private float scaleVolume;
    private float currentUpdateTime = 0f;
    private float[] clipsSampleData;


    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        defaultScale = transform.localScale;
        clipsSampleData = new float[numberOfSamples];
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            ScaleToMusic();
            //Debug.Log(scaleVolume);    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(soundTrigger))
        {
            audioSource.Play();
           
            isPlaying = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(soundTrigger))
        {
            audioSource.Stop();
            isPlaying = false;

            StopScalingToMusic();
        }
    }

    private void ScaleToMusic()
    {
        //transform.localScale = defaultScale;
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateTime)
        {
            currentUpdateTime = 0f;
            //music.clip.GetData(clipsSampleData, music.timeSamples);
            audioSource.clip.GetData(clipsSampleData, audioSource.timeSamples);

            scaleVolume = 0f;
            foreach (float sample in clipsSampleData)
            {
                scaleVolume += sample;
            }
            scaleVolume /= numberOfSamples;

            scaleVolume = Mathf.Clamp(scaleVolume, minDeltaSize, maxDeltaSize);
            transform.localScale = defaultScale * (1 + scaleVolume);
        }
    }

    private void StopScalingToMusic()
    {
        transform.localScale = defaultScale;
    }

}
