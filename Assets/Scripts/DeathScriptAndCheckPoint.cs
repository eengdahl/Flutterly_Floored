using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptAndCheckPoint : MonoBehaviour
{
    public Vector3 respawnPoint;
    [SerializeField] Transform startRespawnPoint;
    //public Vector3 checkPoint;

    private void Start()
    {
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
    }
}
