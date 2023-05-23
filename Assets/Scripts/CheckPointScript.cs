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

    private void Awake()
    {
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

                mapTutorial.SetActive(true);
                Invoke(nameof(DisableMap), 3);
            }
        }

    }

    void DisableMap()
    {
        mapTutorial.SetActive(false);
    }
}
