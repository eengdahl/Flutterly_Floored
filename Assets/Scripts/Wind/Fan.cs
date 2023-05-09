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

    public bool shouldRotate;


    //Sound
    AudioSource aS;


    //Add got electricity in begining
    public bool gotElectricity;

    private void Start()
    {
        shouldRotate = false;
        rotationSpeed = 600;
        aS = GetComponent<AudioSource>();
        on = true;
        windArea = windAreaOne;
    }


    void Update()
    {
        if (!gotElectricity) return;
        if (on)
        {

            // Rotate the fan blade 
            wholeFanPart.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            //Rotate whole fan base
            if (shouldRotate)
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
        else
        {
            aS.Stop();
        }
    }
    public void TurnOn()
    {
        aS.Play();
        gotElectricity = true;
        if (on)
        {
            windArea.SetActive(true);

        }
    }
    public void TurnOff()
    {
        aS.Stop();
        gotElectricity = false;
        windArea.SetActive(false);
    }
    public void SwitchWindStr()
    {
        if (on)
        {
            windArea.SetActive(false);
            aS.Stop();

        }

        if (windArea == windAreaOne)
        {
            aS.Play();
            aS.pitch = 2f;
            rotationSpeed = 1200;
            windArea = windAreaTwo;
        }
        else if (windArea == windAreaTwo)
        {
            aS.Play();
            aS.pitch = 1.5f;
            rotationSpeed = 600;
            windArea = windAreaOne;
        }

        if (on)
        {
            aS.Play();

            windArea.SetActive(true);
        }
    }
}
