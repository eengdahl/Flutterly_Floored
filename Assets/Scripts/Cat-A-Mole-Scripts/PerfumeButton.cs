using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfumeButton : MonoBehaviour
{

    private float catLives = 3;
    public bool buttonReady;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && buttonReady)
        {
            Debug.Log("sprutsprut");
            
            ButtonPush();
            buttonReady = false;
            
        }
    }

    public void ButtonPush()
    {
        catLives--;
        Debug.Log(catLives);
        if (catLives <= 0)
        {
            Debug.Log("CatIsDiededGGEZ");
        }
        StartCoroutine(ButtonReset());
    }

    private IEnumerator ButtonReset()
    {
        yield return new WaitForSeconds(2f);
        buttonReady = true;
    }
}
