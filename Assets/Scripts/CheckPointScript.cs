using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] Transform RespawnPoint;
    [SerializeField] int index;
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
                activateButtons.SetBoolToTrue(index);
        }

    }
}
