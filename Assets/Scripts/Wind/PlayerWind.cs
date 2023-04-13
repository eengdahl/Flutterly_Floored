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
    GameObject Player;
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
            transform.rotation = other.transform.rotation;
            windZone = other.gameObject;
            Invoke("Fly",0.1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "windArea")
        {
            Invoke("LeaveWindArea", 0.12f);
            //LeaveWindArea();
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
    public void Fly()
    {
        
        EnterWindArea();

        //Set rotation
        
    }
}
