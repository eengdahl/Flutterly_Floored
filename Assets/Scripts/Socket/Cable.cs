using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public bool canConnect;
    public Lamp lamp;
    private void Start()
    {
        canConnect = true;
    }

    public void ChangeLayer()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 6;
        }
    }
    
}
