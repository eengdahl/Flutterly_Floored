using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject secondCamera;

    private void Awake()
    {
        Debug.Log("hej");
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("ping");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ping");
            MainCamera.SetActive(false);
            secondCamera.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {

        Debug.Log("ping");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ping");
            MainCamera.SetActive(true);
            secondCamera.SetActive(false);

        }
    }
}
