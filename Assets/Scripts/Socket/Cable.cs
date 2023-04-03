using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public bool canConnect;
    public Lamp lamp;
    public RadioButton radio;
    public Fan fan;
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
