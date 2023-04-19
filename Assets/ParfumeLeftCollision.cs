using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParfumeLeftCollision : MonoBehaviour
{
    VitrinBrain vitrinBrain;
    HideNSeekBottles bottles;


    private void Start()
    {
        vitrinBrain = FindAnyObjectByType<VitrinBrain>();
        bottles = GetComponentInChildren<HideNSeekBottles>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "VitrinCat" && vitrinBrain.isGazing)
        {
            bottles.catInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "VitrinCat")
        {
            bottles.catInRange = false;
        }
    }
}
