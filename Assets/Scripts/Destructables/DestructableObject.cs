using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public GameObject destructionPrefab;
    public bool canTakeFallDamage;
    public bool canBeShot;
    public float breakThresholdHeight;

    private GameObject destructableObject;
    private Rigidbody rb;
    private float fallheight;
    private bool isFalling;
    private float fallStartPosition;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rb.velocity.y < -0.1f && !isFalling)
        {
            fallStartPosition = transform.position.y;
            isFalling = true;
        }
        if (isFalling)
        {
            CalculateFallheight();
            Debug.Log(fallheight);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (canTakeFallDamage && collision.gameObject.CompareTag("Ground") && isFalling && fallheight > breakThresholdHeight)
        {
            Instantiate(destructionPrefab, transform.position, transform.rotation);
            isFalling = false;
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            isFalling = false;
        }

        if(canBeShot && collision.gameObject.CompareTag("Bullet"))
        {
            destructableObject = Instantiate(destructionPrefab, transform.position, transform.rotation);
            destructableObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            Destroy(this.gameObject);
        }

        
    }

    private void CalculateFallheight()
    {
        fallheight = fallStartPosition - transform.position.y;
    }
}
