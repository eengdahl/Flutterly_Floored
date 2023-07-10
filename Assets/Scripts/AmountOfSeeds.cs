using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountOfSeeds : MonoBehaviour
{
    [SerializeField] GameStats gameStats;

    public int amountOfSeeds;
    private void Start()
    {
        CountObjects();
    }
    void CountObjects()
    {
        GameObject[] objectsWithTag1 = GameObject.FindGameObjectsWithTag("Seed");
        GameObject[] objectsWithTag2 = GameObject.FindGameObjectsWithTag("Yarn");

        // Combine the counts from both arrays
        amountOfSeeds = objectsWithTag1.Length + objectsWithTag2.Length;

        // Print the count to the console (optional)
        //Debug.Log("Object Count: " + amountOfSeeds);

       gameStats.maxAmountOfSeeds = amountOfSeeds;
    }
}
