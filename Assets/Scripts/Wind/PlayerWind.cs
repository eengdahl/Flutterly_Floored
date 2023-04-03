using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWind : MonoBehaviour
{
    public bool inWindZone;
    public GameObject windZone;
    Rigidbody rb;
    JumpTest jumpScript;
    private void Start()
    {
        jumpScript = GetComponent<JumpTest>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "windArea")
        {
           
            windZone = other.gameObject;
            inWindZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        {
            if (other.gameObject.tag == "windArea")
            {
                inWindZone = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (inWindZone)
        {
            jumpScript.glideTime = 10; 
            rb.AddForce(windZone.GetComponent<WindArea>().windDirection.transform.forward * windZone.GetComponent<WindArea>().windStrength);
        }
    }
}
