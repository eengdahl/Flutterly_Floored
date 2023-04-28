using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    //public GameObject fan;
    public bool on;
    private float rotationSpeed = 600f;
    GameObject windArea;
    [SerializeField] GameObject windAreaOne;
    [SerializeField] GameObject windAreaTwo;
    [SerializeField] GameObject wholeFanPart;
    [SerializeField] GameObject baseOfFan;


    //Base of fan rotation
    public float maxAngle = 45f; // Maximum angle of rotation in 
    public float minAngle = -45f; // Minimum angle of rotation in 
    public float rotationSpeedBase = 10f; // Speed of rotation 

    private float targetAngle; // Target angle for rotation
    private float currentAngle; // Current angle of rotation

    [Header("Buttons")]
    [SerializeField] FanButton rotationButton;
    [SerializeField] FanButton strengthButton;

    //Sound
    AudioSource aS;



    private void Start()
    {
        rotationSpeed = 600;
        aS = GetComponent<AudioSource>();
        on = false;
        windArea = windAreaOne;
    }


    void Update()
    {
        if (on)
        {
            // Rotate the fan blade 
            wholeFanPart.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            //Rotate whole fan base
            if (rotationButton.buttonOn)
            {
                float deltaAngle = rotationSpeedBase * Time.deltaTime;
                currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, deltaAngle);
                baseOfFan.transform.localRotation = Quaternion.Euler(currentAngle, -90f, 0f);

                //Make fan base switch between the different angles when it reaches one of them
                //Mathf.Approximately is apparently a great tool to get if the two values are about the same, exactly the same can be hard sometimes
                if (Mathf.Approximately(currentAngle, targetAngle))
                {
                    if (targetAngle == maxAngle)
                    {
                        targetAngle = minAngle;
                    }
                    else
                    {
                        targetAngle = maxAngle;
                    }
                }

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
    public void SwitchWindStr()
    {
        if (on)
        {
            windArea.SetActive(false);

        }

        if (windArea == windAreaOne)
        {

            rotationSpeed = 1200;
            windArea = windAreaTwo;
        }
        else if (windArea == windAreaTwo)
        {
            rotationSpeed = 600;
            windArea = windAreaOne;
        }

        if (on)
        {
            windArea.SetActive(true);
        }
    }
}
