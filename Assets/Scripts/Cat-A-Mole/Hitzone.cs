using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitzone : MonoBehaviour
{
    public bool isPlayerInZone;

    private void Start()
    {
        isPlayerInZone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInZone = false;
    }
}
