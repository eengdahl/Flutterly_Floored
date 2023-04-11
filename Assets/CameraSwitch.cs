using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour, ILookChangeListener
{
    public GameObject MainCamera;
    public GameObject secondCamera;
    public GameObject thirdCamera;

    private bool pausCamera;
    private bool playerInside = false;

    private void OnEnable()
    {
        MovementCommunicator.instance.AddLookListener(this);
    }

    private void OnDisable()
    {
        MovementCommunicator.instance.RemoveLookListener(this);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = true;
            MainCamera.SetActive(false);
            secondCamera.SetActive(true);
            thirdCamera.SetActive(false);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = false;
            MainCamera.SetActive(true);
            secondCamera.SetActive(false);
            thirdCamera.SetActive(false);
        }
    }

    public void OnValueChanged(float speed)
    {
        if (!playerInside || pausCamera)
        {
            return;
        }
        Debug.Log(speed);

        if (speed > 15)
        {
            pausCamera = true;
            Invoke("ResetCamera", 0.5f);

            secondCamera.SetActive(false);
            thirdCamera.SetActive(true);
        }
        else if (speed < -15)
        {
            pausCamera = true;
            Invoke("ResetCamera", 0.5f);

            secondCamera.SetActive(true);
            thirdCamera.SetActive(false);
          
        }
    }

    private void ResetCamera()
    {
        pausCamera = false;
    }


}
