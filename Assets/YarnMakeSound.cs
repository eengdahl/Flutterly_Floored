using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnMakeSound : MonoBehaviour
{
    [SerializeField] float speedThreshold = 5f; 
    AudioSource aS; 

    private Rigidbody rb;
    bool soundPlaying;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
    }

    private void Update()
    {
       
        float currentSpeed = rb.velocity.magnitude;

        
        if (currentSpeed > speedThreshold && !soundPlaying)
        {
            aS.Play();
            Debug.Log("Should make sounds now");
            soundPlaying = true;
        }
        else if(currentSpeed < speedThreshold && soundPlaying)
        {
            aS.Stop();
            soundPlaying = false;
        }
    }
}
