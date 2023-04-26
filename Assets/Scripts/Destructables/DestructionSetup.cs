using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DestructionSetup : MonoBehaviour
{
    List<GameObject> parts;
    private Rigidbody rb;

    public float explosionPower;
    public float explosionRadius;
    public float upwatdModifier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        parts = new List<GameObject>();
        foreach(Transform part in transform)
        {
            part.gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = part.GetComponent<Rigidbody>();
            rb.useGravity = true;
            //part.gameObject.AddComponent<BoxCollider>();
            part.gameObject.AddComponent<MeshCollider>();
            part.gameObject.GetComponent<MeshCollider>().convex = true;
            parts.Add(part.gameObject);
            part.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;
        }

        foreach(GameObject partObject in parts)
        {
            partObject.GetComponent<Rigidbody>().mass = transform.GetComponent<Rigidbody>().mass / parts.Count;
        }

        foreach(GameObject partObject in parts)
        {
            partObject.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, transform.position, upwatdModifier);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
