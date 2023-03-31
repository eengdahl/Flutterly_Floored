using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] Light lamplight;
    private void Start()
    {
        lamplight.enabled = false;
    }

    public void TurnOnLight()
    {
        lamplight.enabled = true;
    }
    public void TurnOffLight()
    {
        lamplight.enabled = false;
    }



}
