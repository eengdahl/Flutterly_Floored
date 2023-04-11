using UnityEngine;

public class ClimbingTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player character
        if (other.CompareTag("Player") )
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