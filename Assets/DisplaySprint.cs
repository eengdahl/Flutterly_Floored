using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySprint : MonoBehaviour
{

    [SerializeField] GameObject sprintCanvas;
    bool hasShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!hasShown)
        {
            sprintCanvas.SetActive(true);
            Invoke(nameof(DeactivateSprint), 4f);
            hasShown = true;
        }
    }
    void DeactivateSprint()
    {
        sprintCanvas.SetActive(false);
    }
}
