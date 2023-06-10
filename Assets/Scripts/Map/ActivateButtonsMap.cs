using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateButtonsMap : MonoBehaviour
{

    public bool[] onOrOff;
    [SerializeField] GameObject[] buttons;
    int index;
    private void Start()
    {
        index = 0;
    }
    public void ActiveCheckpoints()
    {
        foreach (GameObject go in buttons)
        {
            go.SetActive(onOrOff[index]);
            index++;
        }
        index = 0;
    }
    public void SetBoolToTrue(int onOffIndex)
    {
        onOrOff[onOffIndex] = true;
    }

}


