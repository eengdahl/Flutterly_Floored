using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenFan : MonoBehaviour
{
    public bool inGawkArea;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGawkArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGawkArea = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
