using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCataMole : MonoBehaviour
{
    [Header("KillCatStuff")]
    [SerializeField] GameObject cat;
    [SerializeField] GameObject hitSpots;
    //[SerializeField] GameObject legs;
    bool hasSpawned;
    private void Start()
    {
        hasSpawned = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (hasSpawned) return;
        if (other.CompareTag("Player"))
        {
            SpawnCat();
            hasSpawned = true;
        }
    }

    void SpawnCat()
    {
        //legs.SetActive(true);
        hitSpots.SetActive(true);
        cat.SetActive(true);
    }
}
