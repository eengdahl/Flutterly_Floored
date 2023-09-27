using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTwo : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject secondCamera;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ping");
            secondCamera.SetActive(true);
            MainCamera.SetActive(false);


            secondCamera.transform.RotateAround(transform.position, transform.up, 180f);

        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ping");
            MainCamera.SetActive(true);
            secondCamera.SetActive(false);

        }
    }
}