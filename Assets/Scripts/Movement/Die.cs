using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Die : MonoBehaviour
{
    public Rigidbody rb;
    public ForceMovement movement;
    public Jump jump;
    public float maxFallheight;


    public bool isFalling;
    private float startFall;
    private float fallheight;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<ForceMovement>();
        jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y < 0 && !isFalling)
        {
            StartFall();
        }

        if(isFalling)
        {
            CalculateFallHeight();

            if(fallheight >= maxFallheight)
            {
                KillPlayer();
            }
        }

        Debug.Log(fallheight);

        if(isFalling && rb.velocity.y >= 0)
        {
            ResetFall();
        }
    }

    private void CalculateFallHeight()
    {
        fallheight = startFall - transform.position.y;
    }

    private void StartFall()
    {
        startFall = transform.position.y;
        isFalling = true;
    }

    private void ResetFall()
    {
        isFalling = false;
        fallheight = 0;
    }

    private void KillPlayer()
    {
        jump.enabled = false;
        movement.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
    }
}
