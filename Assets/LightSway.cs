using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LightSway : MonoBehaviour
{
    Light Light;
    private float standardLight = 2662;
    private float currentLight;


    Transform lightTransform;

    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light>();
        lightTransform = GetComponent<Transform>();
        // Light.colorTemperature
    }

    // Update is called once per frame
    void Update()
    {
        currentLight = Mathf.SmoothStep(2100, 3100, Mathf.PingPong(Time.time / 10, 1));
        Light.colorTemperature = currentLight;


        //Sol g�r ner. Stannar i botten 
        //float rY = Mathf.SmoothStep(34.8f, 40, Mathf.PingPong(Time.time  / 100, 1));
        //lightTransform.rotation = Quaternion.Euler(rY, -83.18f, 0);



    }
}
