using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TriggerVitrinScript : MonoBehaviour
{
    AudioSource aS;
    AudioManager audioManager;
    public AudioClip crash;
    public AudioClip bossMusic;
    VitrinBrain catBrain;
    bool startLock = false;


    // Start is called before the first frame update
    void Start()
    {
        catBrain = FindObjectOfType<VitrinBrain>();
        audioManager = FindObjectOfType<AudioManager>();
        aS = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !startLock)
        {
            aS.PlayOneShot(crash);
            Invoke("TriggerMusic", crash.length + 0.5f);
            startLock = true;
        }
    }

    private void TriggerMusic()
    {
        audioManager.TurnOfMainMusic();
        aS.clip = bossMusic;
        aS.Play();
        TriggerCat();
    }

    private void TriggerCat()
    {
        catBrain.vitrinState = VitrinStates.Patrol;
    }
}
