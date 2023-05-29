using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Defeated : MonoBehaviour
{
    [SerializeField] GameObject originalCamera;


    public void Defeat()
    {
        originalCamera.SetActive(true);
        transform.gameObject.SetActive(false);
    }

}
