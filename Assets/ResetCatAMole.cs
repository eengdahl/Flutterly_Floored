using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCatAMole : MonoBehaviour
{
    [SerializeField] GameObject spoon;
    Vector3 spoonPosition;
    Quaternion spoonRotation;

    private void Start()
    {
        spoonRotation = spoon.transform.rotation;
        spoonPosition = spoon.transform.position;
    }
    public void ResetSpoon()
    {
        spoon.transform.position = spoonPosition;
        spoon.transform.rotation = spoonRotation;
    }
}
