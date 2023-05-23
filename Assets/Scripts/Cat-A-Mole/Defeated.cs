using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeated : MonoBehaviour
{
   
    public void Defeat()
    {
        transform.gameObject.SetActive(false);
    }
    
}
