using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] Transform RespawnPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<DeathScriptAndCheckPoint>().NewCheckpoint(RespawnPoint.transform.position);
        }
       
    }
}
