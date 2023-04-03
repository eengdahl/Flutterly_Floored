using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript2 : MonoBehaviour
{
    bool inside = false;
    public Transform player;
    public float speedUpDown = 3.2f;
    public TestMovement movementScript; //change depending on the type of movement script
    Rigidbody playerRigidbody;
    Jump jumpScript;
    GameObject teleportPoint;
    MaterialCheck materialCheckScript;
    bool RunUpdate = false;
    void Start()
    {
        materialCheckScript = GetComponent<MaterialCheck>();
        jumpScript = GetComponent<Jump>();
        playerRigidbody = GetComponent<Rigidbody>();
        movementScript = GetComponent<TestMovement>();
        inside = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            RunUpdate = true;
            playerRigidbody.velocity = Vector3.zero;
            movementScript.enabled = false;
            inside = !inside;
            //playerRigidbody.isKinematic = true;
            playerRigidbody.useGravity = false;
            jumpScript.enabled = false;
        }
        if (other.gameObject.tag == "GoDownLadder")
        {        
            if (inside == false)
            {
               teleportPoint = other.gameObject.GetComponent<GoDownTrigger>().teleportPoint;
               transform.position = teleportPoint.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Ladder")
        {
            RunUpdate = false;
            playerRigidbody.useGravity = true;
            //playerRigidbody.isKinematic = false;
            movementScript.enabled = true;
            inside = !inside;
            jumpScript.enabled = true;
        }
    }
    void Update()
    {
        if (RunUpdate == false) return;
        if (inside == true && Input.GetKey("w"))
        {
            player.transform.position += Vector3.up / speedUpDown;
        }
        if (inside == true && Input.GetKey("s"))
        {
            player.transform.position += Vector3.down / speedUpDown;
        }
        if (inside == true && Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddForce(-transform.forward * 100000f);
        }



        //Do raycast from player and check if grounded
    }
}
