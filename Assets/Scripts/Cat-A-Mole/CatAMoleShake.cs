using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CatAMoleShake : MonoBehaviour
{
    public CinemachineVirtualCamera catAMoleCam;
    private CinemachineBasicMultiChannelPerlin cameraPerlinNoise;

    public float shakePower;
    public float shakeTime;

    // Start is called before the first frame update
    void Start()
    {
        cameraPerlinNoise = catAMoleCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Debug.Log(cameraPerlinNoise);
    }

    public void ShakeCam()
    {
        if (catAMoleCam != null)
        {
            cameraPerlinNoise.m_AmplitudeGain = shakePower;
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        yield return new WaitForSeconds(shakeTime);

        cameraPerlinNoise.m_AmplitudeGain = 0;
    }


}
