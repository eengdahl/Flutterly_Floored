using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPickup : MonoBehaviour
{
    SeedCounter seedCounter;
    Animator anim;
    AudioSource aS;
    public int seedValue = 1;
    private Transform backpack;
    private GameObject seedParent;
    // Start is called before the first frame update
    void Start()
    {
        seedParent = gameObject.transform.parent.gameObject;
        backpack = GameObject.Find("BackpackPosition").GetComponent<Transform>();
        seedCounter = GameObject.Find("SeedDisplay").GetComponent<SeedCounter>();
        anim = gameObject.GetComponent<Animator>();
        aS = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seedCounter.AddSeed(seedValue);
            seedParent.transform.position = Vector3.zero;
            seedParent.transform.SetParent(backpack, false);
            aS.Play();
            anim.CrossFade("PickedUp", 0);
            Invoke(nameof(UpdateSeedUI), 2);
            Invoke(nameof(DestroyObject), 2);
        }
    }

    private void DestroyObject()
    {
        Destroy(seedParent);
    }
    private void UpdateSeedUI()
    {
        seedCounter.seedCountText.enabled = true;
        
        if (seedCounter != null)
        {
            seedCounter.SetSeedCount(seedCounter.GetSeedCount());

        }
    }
}
