using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] Transform RespawnPoint;

    public Die dieOfFalling;

    private void Start()
    {
        dieOfFalling = FindObjectOfType<Die>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!dieOfFalling.isDead)
            {
                other.GetComponent<DeathScriptAndCheckPoint>().NewCheckpoint(this.RespawnPoint.transform);

            }
        }

    }
}
