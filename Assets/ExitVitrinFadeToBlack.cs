using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitVitrinFadeToBlack : MonoBehaviour
{
    [SerializeField] public VitrinBrain2 vitrinBrain;
    private GameObject player;
    private DeathScriptAndCheckPoint deathScript;
    private SwitchControls switchControls;


    // Start is called before the first frame update
    void Start()
    {
        switchControls = FindAnyObjectByType<SwitchControls>();
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();
        player = GameObject.FindGameObjectWithTag("Player");
        // vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
        // vitrinBrain = GameObject.FindGameObjectWithTag("VitrinCat").GetComponent<VitrinBrain2>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && vitrinBrain.catIsDead)
        {
            switchControls.SwitchToNoInput();
            StartCoroutine(deathScript.FadeToBlack());
            Invoke(nameof(TPPlayer), 3);
        }
    }

    private void TPPlayer()
    {
        player.transform.position = new Vector3(-138.45f, 10.707f, 117.625f);
        StartCoroutine(deathScript.FadeToBlack(false));

       

    }
}
