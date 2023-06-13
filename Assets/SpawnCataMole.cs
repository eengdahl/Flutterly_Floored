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
    [SerializeField] GameObject floor;

    [SerializeField] float distanceToCat;
    [SerializeField] float heightToCat;
    [SerializeField] float despawnDistance;
    [SerializeField] float despawnHeight;
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
        CalculateDistance();
        if (hasBeenActivated)
        {

            if (distanceToCat > despawnDistance && heightToCat < despawnHeight && hasSpawned)
            {
                //Debug.Log("running Despawn");
                DespawnCat();
                idleCat.SetActive(false);
                hasSpawned = false;
                hitSpots.GetComponent<HitZones>().attackAnimator.SetBool("Idling", false);
                hitSpots.GetComponent<HitZones>().catIsActive = false;
            }
            else if (distanceToCat < despawnDistance && heightToCat > despawnHeight && !hasSpawned)
            {
                //Debug.Log("Running Spawn");
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
        //Debug.Log("Spawned Cat");
    }

    void DespawnCat()
    {
        hitSpots.SetActive(false);
        cat.SetActive(false);
    }

    private void CalculateDistance()
    {
        distanceToCat = (player.transform.position - cat.transform.position).magnitude;
        heightToCat = player.transform.position.y - cat.transform.position.y;
        despawnDistance = (transform.position - cat.transform .position).magnitude;
        despawnDistance += 5; //little longer than spawn detect area
        despawnHeight = (floor.transform.position.y - cat.transform.position.y) / 2;
    }
    
}
