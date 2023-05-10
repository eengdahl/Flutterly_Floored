using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptAndCheckPoint : MonoBehaviour
{
    public Transform respawnTransform;
    public Vector3 respawnPoint;
    [SerializeField] Transform startRespawnPoint;
    [SerializeField] GameObject birdBody;
    [SerializeField]BirdCableMovement birdCableMovement;
    [SerializeField] CameraFade cameraFade;
    public GameObject featherPuff;
    PlayerMove playerMoveScript;
    PlayerJump playerJumpScript;
    public AudioClip death;
    AudioSource aS;
    private bool canDie = true;


    //public Vector3 checkPoint;
    Rigidbody rb;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        playerMoveScript = gameObject.GetComponent<PlayerMove>();
        playerJumpScript = gameObject.GetComponent<PlayerJump>();
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
        if (canDie)
        {
            canDie = false;
            aS.PlayOneShot(death);
            // Fade();
            //Invoke(nameof(Fade), 1f);
            playerMoveScript.enabled = false;
            playerJumpScript.enabled = false;
            birdCableMovement.DisableClimbing();
            Invoke("ResetRB", 0.5f);
            FeatherPuff();
            Invoke(nameof(DelayedDeath), 2);
        }
    }
    public void Teleport()
    {
        birdCableMovement.DisableClimbing();
        Invoke("ResetRB", 0.5f);
        transform.rotation = respawnTransform.rotation;
        transform.position = respawnTransform.position;
    }
    void ResetRB()
    {
        birdBody.transform.rotation = transform.rotation;
        rb.isKinematic = false;
    }
    void Fade()
    {
        cameraFade.Fade();
    }

    private void FeatherPuff()
    {
        birdBody.SetActive(false);
        Instantiate(featherPuff, gameObject.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
    }

    public void DelayedDeath()
    {
        transform.rotation = respawnTransform.rotation;
        transform.position = respawnTransform.position;
        MovementCommunicator.instance.NotifyDeathListeners(true);
        birdBody.SetActive(true);
        playerMoveScript.enabled = true;
        playerJumpScript.enabled = true;
        canDie = true;
    }
}
