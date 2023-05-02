using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCupboardEvent : MonoBehaviour
{

    private SwitchControls switchControls;
    VitrinBrain2 vitrinBrain;
    public GameObject vitrinCat;
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
            vitrinCat.SetActive(true);
            // vitrinCat.GetComponent<BoxCollider>().enabled = true;
            vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
            vitrinBrain.activeState = VitrinState.Wake;
            switchControls.SwitchToNoInput();


            //MeshRenderer[] rs = vitrinCat.GetComponentsInChildren<MeshRenderer>();
            //foreach (Renderer r in rs)
            //    r.enabled = false;


            locker = false;
        }
    }
}
