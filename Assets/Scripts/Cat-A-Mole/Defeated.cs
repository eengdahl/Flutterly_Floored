using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Defeated : MonoBehaviour
{
    [SerializeField] GameObject originalCamera;
    [SerializeField] GameObject catAMoleCam;
    [SerializeField] GameObject windowBlock;
    CamCatAMole camSwap;

    private void Start()
    {
        camSwap = GetComponent<CamCatAMole>();
    }


    public void Defeat()
    {
        originalCamera.SetActive(true);
        windowBlock.SetActive(false);
       // camSwap.DisableZone();
        gameObject.SetActive(false);
    }

}
