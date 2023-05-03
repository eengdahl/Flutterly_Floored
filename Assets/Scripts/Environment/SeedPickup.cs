using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPickup : MonoBehaviour
{
    SeedCounter seedCounter;
    public int seedValue = 1;
    public GameObject seedUI;
    public Transform seedUIHolder;
    // Start is called before the first frame update
    void Start()
    {
        seedCounter = GameObject.Find("SeedCounter").GetComponent<SeedCounter>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seedCounter.AddSeed(seedValue);
            UpdateSeedUI();
            Destroy(gameObject);
        }
    }

    private void UpdateSeedUI()
    {
        GameObject ui = Instantiate(seedUI, seedUIHolder);
        
        if (seedCounter != null)
        {
            seedCounter.SetSeedCount(seedCounter.GetSeedCount());
        }
    }
}
