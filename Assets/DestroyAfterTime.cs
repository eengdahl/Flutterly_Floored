using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float destroyTimer;

    private void OnDestroy()
    {
        Destroy(gameObject,destroyTimer);
    }

}
