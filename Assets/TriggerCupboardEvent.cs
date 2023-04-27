using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCupboardEvent : MonoBehaviour
{

    private SwitchControls switchControls;
    VitrinBrain2 vitrinBrain;
    bool locker;

    // Start is called before the first frame update
    void Start()
    {
        switchControls = FindAnyObjectByType<SwitchControls>();
        locker = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && locker)
        {

            vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
            vitrinBrain.activeState = VitrinState.Wake;
            switchControls.SwitchToNoInput();
            locker = false;
        }
    }
}
