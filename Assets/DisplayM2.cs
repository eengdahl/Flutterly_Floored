using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayM2 : MonoBehaviour
{

    [SerializeField]GameObject m2Canvas;
    bool hasShown = false;
    
    public bool show = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&show)
        {
            
            m2Canvas.SetActive(true);
            hasShown = true;
            Invoke(nameof(DeactivateM2),5f);
            
        }
    }
 
    public void DeactivateM2()
    {
        m2Canvas.SetActive(false);
        
    }
}
