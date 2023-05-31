using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerCupboardEvent : MonoBehaviour
{

    private SwitchControls switchControls;
    LightSway mainLight;
    VitrinBrain2 vitrinBrain;
    public GameObject vitrinCat;
    bool locker;
    public GameObject blocker;
    public GameObject light0;
    public GameObject light1;
    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        mainLight = FindAnyObjectByType<LightSway>();
        switchControls = FindAnyObjectByType<SwitchControls>();
        locker = true;
    }

    public void TriggerVitrinEvent()
    {
        if (locker)
        {

            StartCoroutine("TriggerOrderVitrin");
            //mainLight.SetRoomDark();
            //blocker.SetActive(true);
            //vitrinCat.SetActive(true);
            //light0.SetActive(true);
            //light1.SetActive(true);
            //vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
            //vitrinBrain.activeState = VitrinState.Wake;
            //switchControls.SwitchToNoInput();

            locker = false;
        }
    }
    IEnumerator TriggerOrderVitrin()
    {
        mainLight.SetRoomDark();
        blocker.SetActive(true);
        vitrinCat.SetActive(true);
        light0.SetActive(true);
        light1.SetActive(true);
        //Set player position 
        player.transform.position = new Vector3(-138.61f, 10.667f, 100.3451f);
        switchControls.SwitchToNoInput();
        yield return new WaitForSeconds(1);
        vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
        vitrinBrain.activeState = VitrinState.Wake;
        yield return 0;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player" && locker)
    //    {
    //        mainLight.SetRoomDark();
    //        blocker.SetActive(true);
    //        vitrinCat.SetActive(true);
    //        light0.SetActive(true);
    //        light1.SetActive(true);
    //        vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
    //        vitrinBrain.activeState = VitrinState.Wake;
    //        switchControls.SwitchToNoInput();

    //        locker = false;
    //    }
    //}
}
