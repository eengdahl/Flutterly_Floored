using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableSleep : MonoBehaviour
{
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            rb.WakeUp();
            StartCoroutine(InactivatePhysics());
        }
    }

    IEnumerator InactivatePhysics()
    {
        yield return new WaitForSeconds(10);
        rb.Sleep();
    }
}
