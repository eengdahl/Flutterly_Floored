using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovementScript : MonoBehaviour
{
    Rigidbody rb;
    PlayerWind playerWindScript;
    
    private void Awake()
    {
        playerWindScript = GetComponent<PlayerWind>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //rb.AddForce(playerWindScript.windZone.GetComponent<WindArea>().windDirection.transform.forward * playerWindScript.windZone.GetComponent<WindArea>().windStrength);
        //rb.AddForce(playerWindScript.windZone.GetComponent<WindArea>().windDirection.transform.up * playerWindScript.windZone.GetComponent<WindArea>().windStrengthUp);
    }
}
