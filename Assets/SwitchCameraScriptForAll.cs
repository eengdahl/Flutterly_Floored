using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCameraScriptForAll : MonoBehaviour
{
    [SerializeField] GameObject originalCamera;
    [SerializeField] GameObject cupboardCamera;
    private AudioSource aS;
    [SerializeField] AudioClip catScream;

    private GameObject player;
    private bool locker = false;
    LightSway mainLight;
    DeathScriptAndCheckPoint deathScript;
    TriggerCupboardEvent triggerVitrin;
    private SwitchControls switchControls;


    private void Start()
    {
        aS = gameObject.AddComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        switchControls = FindAnyObjectByType<SwitchControls>();
        mainLight = FindAnyObjectByType<LightSway>();
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();
        triggerVitrin = FindAnyObjectByType<TriggerCupboardEvent>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            if (!locker)
            {
                //Make first cat sound
                aS.clip = catScream;
                aS.Play();
                switchControls.SwitchToNoInput();
                StartCoroutine(deathScript.FadeToBlack());
                Invoke(nameof(ResetFadeToBlack), 3);
                locker = true;
            }
            Invoke("SwitchCamOn", 2f);
            //    Invoke("SwitchCamera", 0.1f);

        }
    }
    public void ResetFadeToBlack()
    {
        player.transform.position = new Vector3(-137.9307f, 10.667f, 100.5542f);
        StartCoroutine(deathScript.FadeToBlack(false));
        Invoke(nameof(LastResetFromBlack), 1.5f);
    }

    private void LastResetFromBlack()
    {
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
