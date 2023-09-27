using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSoundWhenHittingFloor : MonoBehaviour
{

    private bool isFalling;
    private bool hasPlayedSound = false;
    private float fallheight;
    private float fallStartPosition;
    public float soundThresholdHeight;
    
    private Rigidbody rb;
    private AudioSource aS;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isFalling && fallheight > soundThresholdHeight && !hasPlayedSound)
        {
            aS.Play();
            hasPlayedSound = true;
        }
    }




    void Update()
    {
        if (rb.velocity.y < -0.1f && !isFalling)
        {
            fallStartPosition = transform.position.y;
            isFalling = true;
        }
        if (isFalling)
        {
            CalculateFallheight();
        }
    }
    private void CalculateFallheight()
    {
        fallheight = fallStartPosition - transform.position.y;
    }
}
