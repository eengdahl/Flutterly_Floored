using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPickup : MonoBehaviour
{
    SeedCounter seedCounter;
    Animator anim;
    AudioSource aS;
    public int seedValue = 1;
    public Transform seedUIHolder;
    // Start is called before the first frame update
    void Start()
    {
        seedCounter = GameObject.Find("SeedDisplay").GetComponent<SeedCounter>();
        anim = gameObject.GetComponent<Animator>();
        aS = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seedCounter.AddSeed(seedValue);

            aS.Play();
            anim.CrossFade("PickedUp", 0);
            Invoke(nameof(UpdateSeedUI), 1);
            Invoke(nameof(DestroyObject), 1);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
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
