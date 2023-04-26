using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBall : MonoBehaviour
{
    private Transform ballTransform;
    private new ParticleSystem particleSystem;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        ballTransform = transform.GetChild(0);
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.transform.position = ballTransform.position + offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        particleSystem.transform.position = ballTransform.position + offset;
    }
}
