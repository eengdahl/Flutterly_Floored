using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfumeButton : MonoBehaviour
{

    private float catLives = 3;
    public bool buttonReady = true;
    AudioSource aS;

    [Header("KillCatStuff")]
    [SerializeField] GameObject cat;
    [SerializeField] GameObject hitSpots;
    [SerializeField] GameObject Legs;
    [SerializeField] GameObject DeathBox;


    private void Start()
    {
          buttonReady = true;
        aS = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && buttonReady)
        {
            Debug.Log("sprutsprut");
            aS.Play();
            
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
            KillCat();
            Debug.Log("CatIsDiededGGEZ");
        }
        StartCoroutine(ButtonReset());
    }

    private IEnumerator ButtonReset()
    {
        yield return new WaitForSeconds(2f);
        buttonReady = true;
    }

    void KillCat()
    {
        Legs.SetActive(false);
        DeathBox.SetActive(false);
        hitSpots.SetActive(false);
        cat.SetActive(false);
    }
}
