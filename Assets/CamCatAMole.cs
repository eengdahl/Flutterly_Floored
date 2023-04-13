using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCatAMole : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject catACam;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            catACam.SetActive(true);
            mainCam.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            catACam.SetActive(false);
            mainCam.SetActive(true);
        }
    }
}
