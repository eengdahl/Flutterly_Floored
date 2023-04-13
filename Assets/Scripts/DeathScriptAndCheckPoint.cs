using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptAndCheckPoint : MonoBehaviour
{
    public Vector3 respawnPoint;
    [SerializeField] Transform startRespawnPoint;
    //public Vector3 checkPoint;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnPoint = startRespawnPoint.position;
    }

    public void NewCheckpoint(Vector3 newRespawnPoint) // Vector3 newCheckpoint,
    {
        //checkPoint = newCheckpoint;
        respawnPoint = newRespawnPoint;
    }

    public void Die()
    {
        transform.position = respawnPoint;
        Invoke("ResetRB", 0.5f);
    }
    void ResetRB()
    {
        rb.isKinematic = false;
    }
}
