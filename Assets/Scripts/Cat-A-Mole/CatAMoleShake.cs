using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CatAMoleShake : MonoBehaviour
{
    public CinemachineVirtualCamera catAMoleCam;
    public AudioClip pawBang;
    public AudioClip[] catScream;
    public AudioSource audioSource2;

    private AudioSource audioSource;
    


    private CinemachineBasicMultiChannelPerlin cameraPerlinNoise;

    public float shakePower;
    public float shakeTime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        cameraPerlinNoise = catAMoleCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //Debug.Log(cameraPerlinNoise);
    }

    public void ShakeCam()
    {
        if (catAMoleCam != null)
        {
            cameraPerlinNoise.m_AmplitudeGain = shakePower;
            StartCoroutine(ShakeCoroutine());
        }
    }

    public void PlayPawSound()
    {
        audioSource.clip = pawBang;
        audioSource.Play();
    }

    public void PlayCatScream()
    {
        float volume = Random.Range(0.8f, 1.0f);
        int randomCatSound = Random.Range(0, catScream.Length);
        audioSource2.pitch = volume;
        audioSource2.clip = catScream[randomCatSound];
        audioSource2.Play();
    }

    private IEnumerator ShakeCoroutine()
    {
        yield return new WaitForSeconds(shakeTime);

        cameraPerlinNoise.m_AmplitudeGain = 0;
    }




}
