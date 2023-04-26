using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TriggerVitrinScript : MonoBehaviour
{
    AudioSource aS;
    AudioManager audioManager;
    public AudioClip crash;
    public AudioClip bossMusic;
    VitrinBrain catBrain;
   public GameObject cat;
    bool startLock = false;
    [SerializeField] ScreenShake screenShake;



    // Start is called before the first frame update
    void Start()
    {
        
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
        cat.SetActive(true);
        catBrain = FindObjectOfType<VitrinBrain>();
        audioManager.TurnOfMainMusic();
        aS.clip = bossMusic;
        aS.Play();
        TriggerCat();
        screenShake.shakeObjectFalls();
    }

    private void TriggerCat()
    {
        catBrain.ShakeMovement();
        catBrain.vitrinState = VitrinStates.Patrol;

    }
}
