using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCameraScriptForAll : MonoBehaviour
{
    [SerializeField] GameObject originalCamera;
    [SerializeField] GameObject cupboardCamera;
    private bool locker;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!locker)
            {
                SwitchCamera();
                locker = true;
                Invoke("LockSwitch", 1);

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!locker)
            {
                originalCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.Orthographic = false;
                SwitchCamera();
                Invoke("LockSwitch", 1);
                locker = true;
            }
        }
    }

    void SwitchCamera()
    {
        originalCamera.SetActive(!originalCamera.activeSelf);
        cupboardCamera.SetActive(!originalCamera.activeSelf);
    }

    private void LockSwitch()
    {
        locker = false;
    }
}
