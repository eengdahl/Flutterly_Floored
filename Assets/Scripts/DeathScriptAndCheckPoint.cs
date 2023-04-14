using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptAndCheckPoint : MonoBehaviour
{
    Transform respawnTransform;
    public Vector3 respawnPoint;
    [SerializeField] Transform startRespawnPoint;
    //public Vector3 checkPoint;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnPoint = startRespawnPoint.position;
    }

    public void NewCheckpoint(Transform newRespawnPoint) // Vector3 newCheckpoint,
    {
        respawnTransform = newRespawnPoint;
        //checkPoint = newCheckpoint;
        respawnPoint = newRespawnPoint.position;

    }

    public void Die()
    {
        transform.rotation = respawnTransform.rotation;
        transform.position = respawnPoint;
        Invoke("ResetRB", 0.5f);
    }
    void ResetRB()
    {
        rb.isKinematic = false;
    }
}
