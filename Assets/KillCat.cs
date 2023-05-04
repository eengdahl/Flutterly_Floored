using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCat : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("CatCatAMole"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
