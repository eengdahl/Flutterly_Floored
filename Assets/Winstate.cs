using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winstate : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
