using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRotation : MonoBehaviour
{

    BirdCableMovement climbMovement;
    public float maxRotation = 45f; // Maximum rotation angle in degrees
    private void Start()
    {
        climbMovement = GetComponent<BirdCableMovement>();
    }
    void LateUpdate()
    {

        if (!climbMovement.isClimbing) return;

        //// Get the current rotation of the character
        //Quaternion currentRotation = transform.rotation;

        //// Clamp the rotation
        //float yAngle;

        //yAngle = Mathf.Clamp(currentRotation.eulerAngles.y, -maxRotation, maxRotation);

        //Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, yAngle, currentRotation.eulerAngles.z);

        //transform.rotation = newRotation;

        if (transform.rotation.eulerAngles.y < 90 && transform.rotation.eulerAngles.y > -90)
        {

        }
    }
}