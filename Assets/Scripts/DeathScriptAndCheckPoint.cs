using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScriptAndCheckPoint : MonoBehaviour
{
    public Transform respawnTransform;
    public Vector3 respawnPoint;
    [SerializeField] Transform startRespawnPoint;
    [SerializeField] GameObject birdBody;
    [SerializeField] BirdCableMovement birdCableMovement;
    [SerializeField] CameraFade cameraFade;
    public GameObject featherPuff;
    PlayerMove playerMoveScript;
    PlayerJump playerJumpScript;
    public AudioClip death;
    AudioSource aS;
    public GameObject fadeToBlackImage;
    private bool canDie = true;


    //Catamole
    [SerializeField] ResetCatAMole resetCatAMole;

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
            resetCatAMole.ResetSpoon();
            canDie = false;
            aS.volume = 1;
            aS.PlayOneShot(death);
            // Fade();
            //Invoke(nameof(Fade), 1f);
            playerMoveScript.enabled = false;
            playerJumpScript.enabled = false;
            birdCableMovement.DisableClimbing();
            Invoke("ResetRB", 0.5f);
            FeatherPuff();
            Invoke(nameof(DelayedDeath), 2);
            StartCoroutine(FadeToBlack());
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
        aS.volume = 0.8f;
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
        StartCoroutine(FadeToBlack(false));
    }

    public IEnumerator FadeToBlack(bool fadeToBlack = true, float fadeSpeed = 1, float delay = 1)
    {
        Color objectColor = fadeToBlackImage.GetComponent<Image>().color;
        float fadeAmount;

        yield return new WaitForSeconds(delay);
        if (fadeToBlack)
        {
            while (fadeToBlackImage.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeToBlackImage.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (fadeToBlackImage.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeToBlackImage.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }

    }
}
