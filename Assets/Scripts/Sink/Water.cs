using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Water : MonoBehaviour
{
    private float waterLevel;
    private float maxWaterLevel;
    private float minWaterLevel;

    private AudioSource aS;
    private int closestVertexIndex = -1;

    public float waterFillSpeed;

    public bool isFilling;
    public bool isDraining;

    public GameObject waterTapPS;
    public GameObject maxWaterPoint;

    public AudioClip fillWater;
    public AudioClip EmptyWater;


    void Start()
    {
        //waterFilter = GetComponent<MeshFilter>();
        //waterPlane = transform.GetComponent<Cloth>();
        //waterMaterial = GetComponent<Renderer>().sharedMaterial;
        //waterTexture = (Texture2D)waterMaterial.GetTexture("_Noise");

        aS = GetComponent<AudioSource>();
        maxWaterLevel = maxWaterPoint.transform.position.y;
        minWaterLevel = transform.position.y;
    }

    private void Update()
    {
        if (isFilling && waterLevel < maxWaterLevel)
        {
            FillWater();
            isDraining = false;
            waterTapPS.SetActive(true);
        }
        else
        {
            isFilling = false;
            waterTapPS.SetActive(false);
        }

        if (isDraining && waterLevel > minWaterLevel)
        {
            DrainWater();
            isFilling = false;
        }
        else
            isDraining = false;

        if (!isFilling && !isDraining)
        {
            aS.Stop();

        }
    }

    private void FillWater()
    {
        if (isFilling && waterLevel < maxWaterLevel && !isDraining)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + waterFillSpeed * 0.005f, transform.position.z);
            waterLevel = transform.position.y;
            PlayFillSound();
        }
    }

    public void DrainWater()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - waterFillSpeed * 0.005f, transform.position.z);
        waterLevel = transform.position.y;
        PlayDrainSound();
    }

    public void PlayFillSound()
    {
        if (!isFilling || aS.isPlaying)
        {
            return;

        }
        aS.PlayOneShot(fillWater);
    }

    public void PlayDrainSound()
    {
        if (!isDraining || aS.isPlaying)
        {

            return;
        }
        aS.PlayOneShot(EmptyWater);
    }

    public float GetSimpleWaterHeight()
    {
        return transform.position.y;
    }
}
