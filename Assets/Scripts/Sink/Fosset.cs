using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fosset : MonoBehaviour
{
    public GameObject water;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "OnButton" && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WaterRise");
            water.GetComponent<Water>().isFilling = true;
        }

        if (gameObject.name == "OffButton" && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WaterLower");
            water.GetComponent<Water>().isDraining = true;
        }
    }
}
