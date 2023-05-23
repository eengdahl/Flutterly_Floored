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
    DeathScriptAndCheckPoint deathScript;
    TriggerCupboardEvent triggerVitrin;
    private void Start()
    {
        mainLight = FindAnyObjectByType<LightSway>();
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();
        triggerVitrin = FindAnyObjectByType<TriggerCupboardEvent>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Invoke("SwitchCamOn", 0.5f);
            StartCoroutine(deathScript.FadeToBlack());
            Invoke(nameof(ResetFadeToBlack), 2);
            //    Invoke("SwitchCamera", 0.1f);

        }
    }
    public void ResetFadeToBlack()
    {
        StartCoroutine(deathScript.FadeToBlack(false));
        triggerVitrin.TriggerVitrinEvent();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            originalCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.Orthographic = false;
            Invoke("SwitchCamOff", 0.1f);
            // Invoke("SwitchCamera", 0.1f);
        }
    }

    void SwitchCamera()
    {

        //if (originalCamera.active==true)
        //{
        //    originalCamera.SetActive(false);
        //    cupboardCamera.SetActive(true);
        //}
        //else if (cupboardCamera.active == true)
        //{
        //    originalCamera.SetActive(true);
        //    cupboardCamera.SetActive(false);
        //}
        originalCamera.SetActive(!originalCamera.activeSelf);
        cupboardCamera.SetActive(!originalCamera.activeSelf);

    }

    void SwitchCamOn()
    {
        originalCamera.SetActive(false);
        cupboardCamera.SetActive(true);
    }
    void SwitchCamOff()
    {
        cupboardCamera.SetActive(false);
        originalCamera.SetActive(true);
    }

}
