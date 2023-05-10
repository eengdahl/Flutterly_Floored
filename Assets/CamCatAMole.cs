using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCatAMole : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject catACam;
    AudioManager audioManager;
    AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            aS.Play();
            audioManager.TurnOfMainMusic();
            catACam.SetActive(true);
            mainCam.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            aS.Stop();
            audioManager.ResumeMainMusic();
            catACam.SetActive(false);
            mainCam.SetActive(true);
        }
    }
}
