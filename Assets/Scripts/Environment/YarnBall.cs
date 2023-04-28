using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBall : MonoBehaviour
{
    private Transform ballTransform;
    private Rigidbody ballRB;
    private new ParticleSystem particleSystem;
    private Vector3 offset;
    private float distanceTraveled;
    // Start is called before the first frame update
    void Start()
    {
        ballRB = GameObject.Find("Ball").GetComponent<Rigidbody>();
        ballTransform = transform.GetChild(0);
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        distanceTraveled = ballRB.velocity.magnitude * Time.deltaTime;
        ballTransform.localScale -= new Vector3(distanceTraveled, distanceTraveled,distanceTraveled) * Time.deltaTime;
        offset = new Vector3(0, -ballTransform.localScale.y / 2 + 0.05f, 0);
        particleSystem.transform.position = ballTransform.position + offset;
    }
}
