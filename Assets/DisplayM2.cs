using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayM2 : MonoBehaviour
{

    [SerializeField]GameObject m2Canvas;
    bool hasShown;
    private void Start()
    {
        hasShown = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!hasShown)
        {
            
            m2Canvas.SetActive(true);
            hasShown = true;
            Invoke(nameof(DeactivateM2),5f);
        }
    }
 
    void DeactivateM2()
    {
        m2Canvas.SetActive(false);
        hasShown = true;
    }
}
