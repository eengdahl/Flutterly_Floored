using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenFan : MonoBehaviour
{
    public KitchenFanButton[] buttonList;
    //public List<GameObject> fanButtons;
    public bool inGawkArea;
    public float glideMultiplier;
    AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGawkArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGawkArea = false;
        }
    }

    public void changeFanSpeed()
    {
        foreach (KitchenFanButton btn in buttonList)
        {
            if (btn.buttonDown == true)
            {
                glideMultiplier = btn.fanMultiplier;
                aS.pitch = btn.fanMultiplier - 1;
            }
        }
    }
}
