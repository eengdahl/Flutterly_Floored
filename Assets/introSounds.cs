using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introSounds : MonoBehaviour
{

    AudioSource aS;
    public AudioClip crash;
    public AudioClip fall;
    public AudioClip flapp;


    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    public void PlayFlapp()
    {
        aS.clip = flapp;
        aS.Play();
    }

    public void PlayCrash()
    {
        aS.Stop();
        aS.PlayOneShot(crash);
    }

    public void PlayFall()
    {
        aS.PlayOneShot(fall);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
