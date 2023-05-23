using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayM2 : MonoBehaviour
{

    [SerializeField]GameObject m2Canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m2Canvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m2Canvas.SetActive(false);
        }
    }
}
