using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OrthographicScript : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;

    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        vCam.m_Lens.Orthographic = true;
        vCam.m_Lens.OrthographicSize = 1.5f;
    }


}
