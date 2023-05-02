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
            foreach (KitchenFanButton btn in buttonList)
            {
                if (btn.buttonDown == true)
                {
                    glideMultiplier = btn.fanMultiplier;
                }
            }
            if (!aS.isPlaying) aS.Play();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGawkArea = false;
            aS.Stop();
        }
    }
}
