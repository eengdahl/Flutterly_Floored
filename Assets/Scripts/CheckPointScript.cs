using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] Transform RespawnPoint;
    [SerializeField] int index;
    [SerializeField] private GameObject mapTutorial;
    // public Die dieOfFalling;
    ActivateButtonsMap activateButtons;
    private bool haveShownM = false;
    AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        activateButtons = FindObjectOfType<ActivateButtonsMap>();
        //  dieOfFalling = FindObjectOfType<Die>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<DeathScriptAndCheckPoint>().NewCheckpoint(RespawnPoint.transform);
            if (index < activateButtons.onOrOff.Length)
            {
                activateButtons.SetBoolToTrue(index);
                if (!haveShownM)
                {
                    mapTutorial.SetActive(true);
                    Invoke(nameof(DisableMap), 3);
                    aS.Play();
                    haveShownM = true;

                }
            }
        }

    }

    void DisableMap()
    {
        mapTutorial.SetActive(false);
    }
}
