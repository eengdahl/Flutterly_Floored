using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour
{
    DeathScriptAndCheckPoint playerDeath;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDeath = other.GetComponent<DeathScriptAndCheckPoint>();
            other.GetComponent<Rigidbody>().isKinematic = false;
            playerDeath.Die();
        }
    }
}
