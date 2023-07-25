using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayM1 : MonoBehaviour
{

    [SerializeField]GameObject m1Canvas;
    bool hasShown = false;
    M1Tutorial m1Tutorial;
    
    public bool show = true;



    private void Awake()
    {
        m1Tutorial = FindObjectOfType<M1Tutorial>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !m1Tutorial.hasPicked)
        {       
            m1Canvas.SetActive(true);
            hasShown = true;
            Invoke(nameof(DeactivateM2),3f);           
        }
    }
 
    public void DeactivateM2()
    {
        m1Canvas.SetActive(false);      
    }
}
