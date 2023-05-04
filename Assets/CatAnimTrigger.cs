using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimTrigger : MonoBehaviour
{
    public HitZones hitzones;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hitzones.catIsActive = true;
        }
    }
}
