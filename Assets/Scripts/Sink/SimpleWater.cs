using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class SimpleWater : MonoBehaviour
{
    public float speed1;
    public float direction1;
    public float amplitude1;
    public float distance1;

    public float speed2;
    public float direction2;
    public float amplitude2;
    public float distance2;

    public float speed3;
    public float direction3;
    public float amplitude3;
    public float distance3;


    public float textureSpeed;

    private float xSpeed;
    private float zSpeed;

    private float xSpeed2;
    private float zSpeed2;

    private float xSpeed3;
    private float zSpeed3;

    private float xOffset;
    private float zOffset;

    private float xOffset2;
    private float zOffset2;

    private float xOffset3;
    private float zOffset3;

    private float waveStrength1;
    private float waveStrength2;
    private float waveStrength3;

    private float totalWaveStrength;



    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.RecalculateBounds();
        Debug.Log(mesh.name);
    }

    // Update is called once per frame
    void Update()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        meshRenderer.material.mainTextureOffset += new Vector2(xSpeed, zSpeed) * textureSpeed * Time.deltaTime;
       
        xSpeed = Mathf.Sin(direction1 * Mathf.Deg2Rad);
        zSpeed = Mathf.Cos(direction1 * Mathf.Deg2Rad);

        xSpeed2 = Mathf.Sin(direction2 * Mathf.Deg2Rad);
        zSpeed2 = Mathf.Cos (direction2 * Mathf.Deg2Rad);

        xSpeed3 = Mathf.Sin(direction3 * Mathf.Deg2Rad);
        zSpeed3 = Mathf.Cos(direction3 * Mathf.Deg2Rad);

        for (var i = 0; i < vertices.Length; i++)
        {
            xOffset = vertices[i].x * xSpeed;
            zOffset = vertices[i].z * zSpeed;

            xOffset2 = vertices[i].x * xSpeed2;
            zOffset2 = vertices[i].z * zSpeed2;

            xOffset3 = vertices[i].x * xSpeed3;
            zOffset3 = vertices[i].z * zSpeed3;

            waveStrength1 = Mathf.Sin(distance1 * (xOffset + zOffset) + Time.time * speed1) * amplitude1;
            waveStrength2 = Mathf.Sin(distance2 *(xOffset2 + zOffset2) + Time.time * speed2) * amplitude2;
            waveStrength3 = Mathf.Sin(distance3 * (xOffset3 + zOffset3) + Time.time * speed3) * amplitude3;

            vertices[i].y = waveStrength1 + waveStrength2 + waveStrength3;
        }
        //Debug.Log(vertices[10].y);
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    
    }

    public float GetXSpeed()
    {
        return xSpeed;
    }

    public float GetZSpeed()
    {
        return zSpeed;
    }

    public float GetWaveStrength()
    {
        return totalWaveStrength;
    }

    //public float GetYAtIndex(float x)
    //{
    //    Vector3 tmpVector = transform.TransformPoint(mesh.vertices[index]);
    //    return tmpVector.y;
    //    return transform.TransformPoint(Mathf.Sin(distance * (xOffset + zOffset) + Time.time * speed) * amplitude);
    //}
}
