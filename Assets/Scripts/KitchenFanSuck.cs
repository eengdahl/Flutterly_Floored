using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenFanSuck : MonoBehaviour
{

    [SerializeField] Transform targetPoint;
    BirdCableMovement cableMovement;
    Rigidbody rb;
    [SerializeField] float maxSpeed = 5f;     // The maximum speed to move
    [SerializeField] float acceleration = 2f; // The rate at which speed increases as distance decreases
    AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cableMovement = other.gameObject.GetComponent<BirdCableMovement>();
            rb = other.gameObject.GetComponent<Rigidbody>();
            aS.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!cableMovement.isClimbing)
            {
                rb.isKinematic = true;
                Vector3 direction = targetPoint.position - other.transform.position;
                float distance = direction.magnitude;

                // Calculate the speed based on the distance to the target
                float speed = Mathf.Clamp(distance * acceleration, 0f, maxSpeed);

                // Move the object towards the target at the calculated speed
                other.transform.position += direction.normalized * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aS.Stop();
        }
    }


}
