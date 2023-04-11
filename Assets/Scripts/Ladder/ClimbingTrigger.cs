using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbingTrigger : MonoBehaviour
{
    PlayerControls input = null;
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    private void Awake()
    {
        input = new PlayerControls();

        

    }


    private void OnTriggerStay(Collider other)
    {
        // Check if the colliding object is the player character
        if (other.CompareTag("Player") && input.Floor.Drag.IsInProgress())
        {

            BirdCableMovement bird = other.GetComponent<BirdCableMovement>();

            // If the character is not climbing, enable climbing and switch to climbing controls
            if (!bird.isClimbing)
            {
                bird.EnableClimbing();
                other.GetComponent<SwitchControls>().SwitchToClimbing();
            }
            // If the character is already climbing, disable climbing and switch to floor controls
            else
            {
                bird.DisableClimbing();
                other.GetComponent<SwitchControls>().SwitchToFloor();
            }
        }
    }

}