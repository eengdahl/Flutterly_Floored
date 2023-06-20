using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedOfCamera : MonoBehaviour
{

    [SerializeField] FreeFlyCamera flyCamera;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            flyCamera._movementSpeed += 0.25f;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            flyCamera._movementSpeed -= 0.25f;
        }
    }
}
