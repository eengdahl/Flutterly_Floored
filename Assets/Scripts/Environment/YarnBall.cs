using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBall : MonoBehaviour
{
    [SerializeField] GameObject ball;
    private Transform ballTransform;
    public Rigidbody ballRB;
    private new ParticleSystem particleSystem;
    private Vector3 offset;
    private float ballDirection;
    private float distanceTraveled;
    private Vector3 psRotation;
    // Start is called before the first frame update
    void Start()
    {
        //ballRB = GameObject.Find("Ball").GetComponent<Rigidbody>();
        ballTransform = transform.GetChild(0);
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //psRotation = new Vector3(particleSystem.startRotation3D.x, particleSystem.startRotation3D.y, particleSystem.startRotation3D.z);
        ballDirection = ballRB.velocity.magnitude;
        //psRotation = new Vector3(0, ballDirection, 0);
        distanceTraveled = ballRB.velocity.magnitude * Time.deltaTime;
        ballTransform.localScale -= new Vector3(distanceTraveled, distanceTraveled,distanceTraveled) * Time.deltaTime * (1 / ballTransform.localScale.x);
        offset = new Vector3(0, -ballTransform.localScale.y / 2 + 0.05f, 0);
        if (ball.activeSelf == true)
        {
            particleSystem.transform.position = ballTransform.position + offset;
        }
        else
        {
            return;
        }
    }
}
