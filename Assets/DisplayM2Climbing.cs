using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayM2Climbing : MonoBehaviour
{

    [SerializeField]GameObject m2Canvas;
    bool hasShown = false;
    M2tutorial m2Tutorial;
    
    public bool show = true;



    private void Awake()
    {
        m2Tutorial = FindObjectOfType<M2tutorial>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !m2Tutorial.hasDoneClimbing)
        {       
            m2Canvas.SetActive(true);
            hasShown = true;
            Invoke(nameof(DeactivateM2),3f);           
        }
    }
 
    public void DeactivateM2()
    {
        m2Canvas.SetActive(false);      
    }
}
