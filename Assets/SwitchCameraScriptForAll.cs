using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraScriptForAll : MonoBehaviour
{
    [SerializeField] GameObject originalCamera;
    [SerializeField] GameObject cupboardCamera;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        SwitchCamera();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        originalCamera.SetActive(!originalCamera.activeSelf);
        cupboardCamera.SetActive(!originalCamera.activeSelf);
    }
}
