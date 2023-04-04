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
        if (other.gameObject.tag == "Player")
        {
            MainCamera.SetActive(false);
            secondCamera.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MainCamera.SetActive(true);
            secondCamera.SetActive(false);
        }
    }
}
