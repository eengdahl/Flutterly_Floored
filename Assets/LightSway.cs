using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LightSway : MonoBehaviour
{
    Light Light;
    private float standardLightIntensity;
    private float lightIntensityWhenLocked;
    private float currentLight;
    public bool lockLight;
    public bool vitrinLight;
    public bool resetLight;



    Transform lightTransform;
    public GameObject spotLight;
    // Start is called before the first frame update
    void Start()
    {
        vitrinLight = true;
        Light = GetComponent<Light>();
        lightTransform = GetComponent<Transform>();
        standardLightIntensity = Light.intensity;
    }

    private void FixedUpdate()
    {
        if (resetLight)
        {
            Light.intensity = Mathf.Lerp(Light.intensity, lightIntensityWhenLocked, 1f * Time.deltaTime);
            if (Light.intensity == lightIntensityWhenLocked)
            {
                spotLight.SetActive(true);
                resetLight = false;
                Debug.Log("Ping");  
            }
        }

        if (vitrinLight)
        {
            return;
        }


        Light.intensity = Mathf.Lerp(Light.intensity, 0, 10f * Time.deltaTime);
        if (Light.intensity == 0)
        {
            vitrinLight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (lockLight)
        {
            return;
        }

        currentLight = Mathf.SmoothStep(2100, 3100, Mathf.PingPong(Time.time / 10, 1));
        Light.colorTemperature = currentLight;

        //Sol går ner. Stannar i botten 
        //float rY = Mathf.SmoothStep(34.8f, 40, Mathf.PingPong(Time.time  / 100, 1));
        //lightTransform.rotation = Quaternion.Euler(rY, -83.18f, 0);

    }
    public void SetRoomDark()
    {
        Debug.Log("Dark");
        // Light.intensity = 0;
        //  Light.intensity = Mathf.Lerp(Light.intensity, 0, 10f*Time.deltaTime);
        vitrinLight = false;
        lockLight = true;
        lightIntensityWhenLocked = Light.intensity;
        spotLight.SetActive(false);
    }

    public void ReSetLightInRoom()
    {
       // spotLight.SetActive(true);
        resetLight = true;
        //  Light.intensity = standardLightIntensity;
        lockLight = false;
    }
}
