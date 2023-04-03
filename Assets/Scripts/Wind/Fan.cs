using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    //public GameObject fan;
    public bool on;
    public float rotationSpeed = 40f;
    [SerializeField]GameObject windArea;
    [SerializeField] GameObject wholeFanPart;


    //Base of fan rotation
    public float maxAngle = 45f; // Maximum angle of rotation in 
    public float minAngle = -45f; // Minimum angle of rotation in 
    public float rotationSpeedBase = 10f; // Speed of rotation 

    private float targetAngle; // Target angle for rotation
    private float currentAngle; // Current angle of rotation

    private void Start()
    {
        on = false;
    }


    void Update()
    {
        if (on)
        {
            // Rotate the fan blade around its local z-axis
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            //Rotate whole fan base
            
            float deltaAngle = rotationSpeedBase * Time.deltaTime;
            currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, deltaAngle);
            wholeFanPart.transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);

            // If the fan base has reached the target angle, switch to the other angle
            if (Mathf.Approximately(currentAngle, targetAngle))
            {
                targetAngle = (targetAngle == maxAngle) ? minAngle : maxAngle;
            }
        }
    }
    public void TurnOn()
    {
        on = true;
        windArea.SetActive(true);
    }
    public void TurnOff()
    {
        on = false;
        windArea.SetActive(false);
    }
}
