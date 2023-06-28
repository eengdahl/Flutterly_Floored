using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitVitrinFadeToBlack : MonoBehaviour
{
    [SerializeField] public VitrinBrain2 vitrinBrain;
    [SerializeField] GameObject player;
    private DeathScriptAndCheckPoint deathScript;
    private SwitchControls switchControls;
    bool locker;
    public GameObject vcam;


    // Start is called before the first frame update
    void Start()
    {
        locker = false;
        switchControls = FindAnyObjectByType<SwitchControls>();
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();
        // vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
        // vitrinBrain = GameObject.FindGameObjectWithTag("VitrinCat").GetComponent<VitrinBrain2>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && vitrinBrain.catIsDead && !locker)
        {
            switchControls.SwitchToNoInput();
            StartCoroutine(deathScript.FadeToBlack(true, 0, 0));


            Invoke(nameof(TPPlayer), 3);
            locker = true;
        }
    }

    private void TPPlayer()
    {
        StartCoroutine(deathScript.FadeToBlack(false, 1, 0));
        player.transform.position = new Vector3(-138.45f, 10.707f, 117.625f);

        switchControls.SwitchToFloor();
        locker = false;
        vcam.SetActive(true);
        vcam.transform.position = new Vector3(-140.1954f, 13.33156f, 119.5955f);
        Destroy(gameObject);

    }

}
