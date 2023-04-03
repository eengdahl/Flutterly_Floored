using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWind : MonoBehaviour
{
    public bool inWindZone;
    public GameObject windZone;
    Rigidbody rb;
    JumpTest jumpScript;
    [SerializeField] float mass;
    float oldMass;
    PlayerMoveTest playerMoveScript;
    WindMovementScript windMovement;

    SwitchControls switchControls;
    
    private void Start()
    {
        windMovement = GetComponent<WindMovementScript>();
        playerMoveScript = GetComponent<PlayerMoveTest>();
        jumpScript = GetComponent<JumpTest>();
        rb = GetComponent<Rigidbody>();
        oldMass = rb.mass;

        switchControls = GetComponent<SwitchControls>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "windArea")
        {
            switchControls.SwitchToAir();
            playerMoveScript.enabled = false;
            rb.mass = mass;
            windZone = other.gameObject;
            inWindZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "windArea")
        {
            switchControls.SwitchToFloor();
            windMovement.enabled = false;
            jumpScript.enabled = true;
            playerMoveScript.enabled = true;
            inWindZone = false;
            rb.mass = oldMass;
        }

    }

    private void FixedUpdate()
    {
        if (inWindZone)
        {
            //jumpScript.glideTime = 10; 
            //rb.AddForce(windZone.GetComponent<WindArea>().windDirection.transform.forward * windZone.GetComponent<WindArea>().windStrength);
            //rb.AddForce(windZone.GetComponent<WindArea>().windDirection.transform.up * windZone.GetComponent<WindArea>().windStrengthUp);
        }
    }
}
