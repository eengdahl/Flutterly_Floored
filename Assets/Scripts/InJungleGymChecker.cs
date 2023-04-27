using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InJungleGymChecker : MonoBehaviour
{
    public bool inJungle = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JungleGym"))
        {
            inJungle = true;
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("JungleGym"))
    //    {
    //        inJungle = false;
    //    }
    //}
}
