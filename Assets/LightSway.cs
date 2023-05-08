using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LightSway : MonoBehaviour
{
    Light Light;
    private float standardLightIntensity;
    private float currentLight;
    public bool lockLight;



    Transform lightTransform;
   public GameObject spotLight;
    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light>();
        lightTransform = GetComponent<Transform>();
        standardLightIntensity = Light.intensity;
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
        Light.intensity = 0;
        lockLight = true;
        spotLight.SetActive(false);
    }

    public void ReSetLightInRoom()
    {
        spotLight.SetActive(true);
        Light.intensity = standardLightIntensity;
        lockLight = false;
    }
}
