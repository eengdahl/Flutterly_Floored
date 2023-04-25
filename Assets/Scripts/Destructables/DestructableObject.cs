using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public GameObject destructionPrefab;
    public GameObject destructableObject;
    public bool canTakeFallDamage;
    public bool canBeShot;
 

    private void OnCollisionEnter(Collision collision)
    {
        if (canTakeFallDamage && collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(destructionPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        if(canBeShot && collision.gameObject.CompareTag("Bullet"))
        {
            destructableObject = Instantiate(destructionPrefab, transform.position, transform.rotation);
            destructableObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            Destroy(this.gameObject);
        }
    }
}
