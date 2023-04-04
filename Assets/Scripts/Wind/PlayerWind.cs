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

    SwitchControls switchControls;
    TestFly flyScript;

    private void Start()
    {
        playerMoveScript = GetComponent<PlayerMoveTest>();
        jumpScript = GetComponent<JumpTest>();
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
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "windArea")
        {
            LeaveWindArea();

        }

    }

    private void FixedUpdate()
    {
        if (inWindZone)
        {
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