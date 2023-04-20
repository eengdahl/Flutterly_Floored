using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptAndCheckPoint : MonoBehaviour
{
    Transform respawnTransform;
    public Vector3 respawnPoint;
    [SerializeField] Transform startRespawnPoint;
    [SerializeField] GameObject birdBody;
    [SerializeField]BirdCableMovement birdCableMovement;
    [SerializeField] CameraFade cameraFade;


    //public Vector3 checkPoint;
    Rigidbody rb;

    private void Start()
    {
        respawnTransform = startRespawnPoint;
        rb = GetComponent<Rigidbody>();
        //respawnPoint = startRespawnPoint.position;
        
    }

    public void NewCheckpoint(Transform newRespawnPoint) 
    {
        respawnTransform = newRespawnPoint;
        //checkPoint = newCheckpoint;
        //respawnPoint = newRespawnPoint.position;
    }

    public void Die()
    {
       // Fade();
        //Invoke("Fade", 1f);
        birdCableMovement.DisableClimbing();
        this.transform.rotation = respawnTransform.rotation;
        this.transform.position = respawnTransform.position;
        Invoke("ResetRB", 0.5f);
        MovementCommunicator.instance.NotifyDeathListeners(true);
    }
    void ResetRB()
    {
        birdBody.transform.rotation = this.transform.rotation;
        rb.isKinematic = false;
    }
    void Fade()
    {
        cameraFade.Fade();
    }
}
