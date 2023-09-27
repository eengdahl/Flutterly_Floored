using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutUnlocked : MonoBehaviour
{
    [SerializeField] GameObject shortCutOne;
    [SerializeField] GameObject shortCutTwo;
    bool hasBeenSet;
    private void Start()
    {
        hasBeenSet = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenSet) return;
        if (other.CompareTag("Player"))
        {
            shortCutOne.SetActive(true);
            shortCutTwo.SetActive(true);
            hasBeenSet = true;
        }
    }
}
