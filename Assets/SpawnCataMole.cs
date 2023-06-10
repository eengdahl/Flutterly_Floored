using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCataMole : MonoBehaviour
{
    [Header("KillCatStuff")]
    [SerializeField] GameObject cat;
    [SerializeField] GameObject hitSpots;
    [SerializeField] GameObject idleCat;
    [SerializeField] GameObject player;

    [SerializeField] float distanceToCat;
    [SerializeField] float visableDistance;


    //[SerializeField] GameObject legs;
    bool hasBeenActivated;
    bool hasSpawned;
    private void Start()
    {
        hasBeenActivated = false;
        hasSpawned = false;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {

        if (hasBeenActivated)
        {
            CalculateDistance();
            if (distanceToCat > visableDistance && hasSpawned)
            {
                DespawnCat();
                idleCat.SetActive(false);
                hasSpawned = false;
                hitSpots.GetComponent<HitZones>().attackAnimator.SetBool("Idling", false);
            }
            else if (distanceToCat < visableDistance && !hasSpawned)
            {
                SpawnCat();
                idleCat.SetActive(true);
                hasSpawned = true;
                hitSpots.GetComponent<HitZones>().attackAnimator.SetBool("Idling", true);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenActivated) return;
        if (other.CompareTag("Player"))
        {
            SpawnCat();
            idleCat.SetActive(false);
            hasBeenActivated = true;
            hasSpawned = true;
            hitSpots.GetComponent<HitZones>().attackAnimator.SetBool("Idling", true);      
        }
    }

    void SpawnCat()
    {
        hitSpots.SetActive(true);
        cat.SetActive(true);
        Debug.Log("Spawned Cat");
    }

    void DespawnCat()
    {
        hitSpots.SetActive(false);
        cat.SetActive(false);
    }

    private void CalculateDistance()
    {
        distanceToCat = (player.transform.position - cat.transform.position).magnitude;
        visableDistance = (transform.position - cat.transform .position).magnitude;
    }
    
}
