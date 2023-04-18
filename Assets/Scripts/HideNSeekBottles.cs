using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNSeekBottles : MonoBehaviour
{

    private float catLives = 3;
    public bool buttonReady = true;
    public bool catInRange;
    AudioSource aS;
    GameObject leftBottle;
    GameObject rightBottle;
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        leftBottle = GameObject.Find("PerfumeButtonLeft");
        rightBottle = GameObject.Find("PerfumeButtonRight");
        buttonReady = true;
        aS = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && buttonReady)
        {
            Debug.Log("sprutsprut");
            ButtonPush();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !buttonReady)
        {
            StartCoroutine(ButtonReset());
        }
    }

    public void ButtonPush()
    {
        //catlives reduce if cat is in range and not defeated
        if (catInRange || leftBottle.GetComponent<HideNSeekBottles>().catLives > 1 || rightBottle.GetComponent<HideNSeekBottles>().catLives > 1)
        {
            if (gameObject == leftBottle)
            {
                catLives = rightBottle.GetComponent<HideNSeekBottles>().catLives;
            }
            if (gameObject == rightBottle)
            {
                catLives = leftBottle.GetComponent<HideNSeekBottles>().catLives;
            }
            catLives--;
            aS.Play();
            Debug.Log(catLives);
            if (catLives <= 0)
            {
                Debug.Log("CatIsDiededGGEZ");
            }
        }
        //button push
        buttonReady = false;
        anim.CrossFade("PerfumeButtonPush", 0, 0);
    }

    private IEnumerator ButtonReset()
    {
        yield return new WaitForSeconds(10f);
        anim.CrossFade("PerfumeButtonInflate", 0, 0);
        buttonReady = true;
    }
}
