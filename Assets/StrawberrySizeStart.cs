using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberrySizeStart : MonoBehaviour
{
    float maxSize = 12;
    float minSize = 8;
    float size;
    void Awake()
    {
        size = Random.Range(8f, 12f);
        transform.localScale = new Vector3(size, size, size);
    }
}
