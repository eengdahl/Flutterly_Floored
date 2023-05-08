using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCameraScriptForAll : MonoBehaviour
{
    [SerializeField] GameObject originalCamera;
    [SerializeField] GameObject cupboardCamera;
    private bool locker = false;
    LightSway mainLight;
    private void Start()
    {
           mainLight = FindAnyObjectByType<LightSway>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Invoke("SwitchCamera", 0.1f);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainLight.gameObject.SetActive(false);
            originalCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.Orthographic = false;
            Invoke("SwitchCamera", 0.1f);
            mainLight.SetRoomDark();
        }
    }

    void SwitchCamera()
    {
        originalCamera.SetActive(!originalCamera.activeSelf);
        cupboardCamera.SetActive(!originalCamera.activeSelf);

    }


}
