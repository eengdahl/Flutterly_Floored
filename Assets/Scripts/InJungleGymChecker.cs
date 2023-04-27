using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InJungleGymChecker : MonoBehaviour
{
    public bool inJungle = false;
    [SerializeField]GameObject jungle;
    float distance;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("JungleGym"))
    //    {
    //        inJungle = true;
    //    }
    //}

    private void Update()
    {
        distance = Vector3.Distance(transform.position, jungle.transform.position);
        if(distance >= 30)
        {
            inJungle = false;
        }
        else if (distance<30)
        {
            inJungle = true;
        }

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("JungleGym") && distance > 30)
    //    {
    //        inJungle = false;
    //    }
    //}
}
