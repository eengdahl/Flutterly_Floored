using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Water : MonoBehaviour
{
    private float waterLevel;
    private float maxWaterLevel;
    private float minWaterLevel;
    public float waterFillSpeed;


    public bool isFilling;
    public bool isDraining;

    public GameObject maxWaterPoint;
   

    void Start()
    {
        maxWaterLevel = maxWaterPoint.transform.position.y;
        minWaterLevel = transform.position.y;
    }

    public float GetSimpleWaterHeight()
    {
        return transform.position.y;
    }

    private void Update()
    {
        if (isFilling && waterLevel < maxWaterLevel)
        {
            FillWater();
            isDraining = false;
        }
        else
            isFilling = false;

        if(isDraining && waterLevel > minWaterLevel)
        {
            DrainWater();
            isFilling = false;
        }
        else
            isDraining = false;
    }


    private void FillWater()
    {
        if (isFilling && waterLevel < maxWaterLevel && !isDraining)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + waterFillSpeed * 0.005f, transform.position.z);
            waterLevel = transform.position.y;
        }
    }

    public void DrainWater()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - waterFillSpeed * 0.005f, transform.position.z);
        waterLevel = transform.position.y;
    }
}
