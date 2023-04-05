using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Die : MonoBehaviour
{
    public Rigidbody rb;
    public PlayerMoveTest movement;
    public JumpTest jump;
    public DeathScriptAndCheckPoint revive;
    public float maxFallheight;

    private float torque;

    public bool isFalling;
    private float startFall;
    private float fallheight;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMoveTest>();
        jump = GetComponent<JumpTest>();
        revive = GetComponent<DeathScriptAndCheckPoint>();
        torque = 50;
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
        rb.AddRelativeTorque(new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2)) * torque);

        Invoke("RevivePlayer", 5);
    }

    private void RevivePlayer()
    {
        jump.enabled = true;
        movement.enabled = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        revive.Die();
    }
}
