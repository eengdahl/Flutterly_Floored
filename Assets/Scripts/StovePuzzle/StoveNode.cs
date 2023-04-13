using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveNode : MonoBehaviour
{
    public bool litNode;

    public void ToggleActive()
    {
        litNode = !litNode;
    }

    public void SetAllActive()
    {
        litNode = true;
    }
  
}
