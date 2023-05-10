using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyYarn : MonoBehaviour
{

    [SerializeField] float destroyScale = 0.1914912f;
    [SerializeField] GameObject seedPrefab;
    [SerializeField] ParticleSystem pS;


    private void Update()
    {
        if (transform.localScale.x <= destroyScale)
        {
            spawnSeed();
        }
    }

    public void spawnSeed()
    {
        Instantiate(seedPrefab, transform.position,Quaternion.Euler(new Vector3 (0,0.11f,0)));
        pS.Pause();
        gameObject.SetActive(false);
    }
}
