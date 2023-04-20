using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveNode : MonoBehaviour
{
    [SerializeField] GameObject fireDeathZone;
    public bool litNode;

    public void ToggleActive()
    {
        fireDeathZone.SetActive(!fireDeathZone.activeSelf);
        litNode = !litNode;
    }
    public void SetAllActive()
    {
        litNode = true;
    }
  
}
