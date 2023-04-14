using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Water : MonoBehaviour
{
    private float waterLevel;
    private float maxWaterLevel;
    public float waterFillSpeed;


    public float rippleSpeed = 0.01f;
    public float waveHeight = 0.4f;
    public float waveDistance = 0.01f;

    public bool isFossetOn;

    public GameObject maxWaterPoint;
    //public Transform ocean;

    //Material oceanMaterial;

    //Texture2D oceanTexture;


    public Shader shaderToBake; // This should be set to the procedural shader you want to bake
    public int textureSize = 512; // The size of the baked texture

    void Start()
    {
        maxWaterLevel = transform.position.y + maxWaterPoint.transform.position.y;
        //SetOcean();
    }

    //private void SetOcean()
    //{
    //    oceanMaterial = GetComponent<Renderer>().sharedMaterial;
    //    oceanTexture = (Texture2D)oceanMaterial.GetTexture("_waveNoise");
    //}

    //public float GetHeigthAtPosition(Vector3 position)
    //{
    //    return transform.position.y + oceanTexture.GetPixelBilinear(position.x * waveDistance, position.z * waveDistance + Time.time * rippleSpeed).g * waveHeight * transform.localScale.x;
    //}

    public float GetSimpleWaterHeight()
    {
        return transform.position.y;
    }

    private void Update()
    {
        if (isFossetOn && waterLevel < maxWaterLevel)
        {
            FillWater();
        }
        else
            isFossetOn = false;
    }

    //private void OnValidate()
    //{
    //    if (!oceanMaterial)
    //    {
    //        SetOcean();
    //    }

    //    UpdateOceanMaterial();
    //}

    //private void UpdateOceanMaterial()
    //{
    //    oceanMaterial.SetFloat("_waveDistance", waveDistance);
    //    oceanMaterial.SetFloat("_waveHeight", waveHeight);
    //    oceanMaterial.SetFloat("_rippleSpeed", rippleSpeed);
    //}


    private void FillWater()
    {
        if (isFossetOn && waterLevel < maxWaterLevel)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + waterFillSpeed * 0.005f, transform.position.z);
            waterLevel = transform.position.y;
        }
    }
}
