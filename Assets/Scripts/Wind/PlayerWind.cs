using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWind : MonoBehaviour
{
    public bool inWindZone;
    public GameObject windZone;
    Rigidbody rb;
    PlayerJump jumpScript;
    [SerializeField] float mass;
    float oldMass;
    PlayerMove playerMoveScript;

    SwitchControls switchControls;
    TestFly flyScript;

    private void Start()
    {
        playerMoveScript = GetComponent<PlayerMove>();
        jumpScript = GetComponent<PlayerJump>();
        rb = GetComponent<Rigidbody>();
        oldMass = rb.mass;
        flyScript = GetComponent<TestFly>();
        switchControls = GetComponent<SwitchControls>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "windArea")
        {
            windZone = other.gameObject;
            EnterWindArea();

            //Set rotation
            transform.rotation = other.transform.rotation;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "windArea")
        {
            LeaveWindArea();
        }

    }


    void EnterWindArea()
    {
        switchControls.SwitchToAir();
        flyScript.enabled = true;
        playerMoveScript.enabled = false; 
        inWindZone = true;
        rb.mass = mass;
        jumpScript.enabled = false;

        
    }
    public void LeaveWindArea()
    {
        switchControls.SwitchToFloor();
        flyScript.enabled = false;
        playerMoveScript.enabled = true;
        windZone = null;
        inWindZone = false;
        rb.mass = oldMass;
        jumpScript.enabled = true;
    }
}
