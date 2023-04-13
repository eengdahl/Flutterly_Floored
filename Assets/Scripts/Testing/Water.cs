using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float rippleSpeed = 0.01f;
    public float waveHeight = 0.4f;
    public float waveDistance = 0.01f;

    public Transform ocean;

    Material oceanMaterial;

    Texture2D oceanTexture;


    public Shader shaderToBake; // This should be set to the procedural shader you want to bake
    public int textureSize = 512; // The size of the baked texture

    void Start()
    {
        //SetOcean();
    }

    private void SetOcean()
    {
        oceanMaterial = GetComponent<Renderer>().sharedMaterial;
        oceanTexture = (Texture2D)oceanMaterial.GetTexture("_waveNoise");
    }

    public float GetHeigthAtPosition(Vector3 position)
    {
        return transform.position.y + oceanTexture.GetPixelBilinear(position.x * waveDistance, position.z * waveDistance + Time.time * rippleSpeed).g * waveHeight * transform.localScale.x;
    }

    public float GetSimpleWaterHeight()
    {
        return transform.position.y;
    }

    //private void OnValidate()
    //{
    //    if (!oceanMaterial)
    //    {
    //        SetOcean();
    //    }

    //    UpdateOceanMaterial();
    //}

    private void UpdateOceanMaterial()
    {
        oceanMaterial.SetFloat("_waveDistance", waveDistance);
        oceanMaterial.SetFloat("_waveHeight", waveHeight);
        oceanMaterial.SetFloat("_rippleSpeed", rippleSpeed);
    }
}
