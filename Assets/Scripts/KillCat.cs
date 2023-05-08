using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OntriggerEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.CompareTag("CatCatAMole"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
