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
    VitrinBrain vitrinBrain;
    BoxCollider parentCollider;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        leftBottle = GameObject.Find("PerfumeButtonLeft");
        rightBottle = GameObject.Find("PerfumeButtonRight");
        buttonReady = true;
        aS = GetComponent<AudioSource>();
        vitrinBrain = FindAnyObjectByType<VitrinBrain>();
        parentCollider = GetComponentInParent<BoxCollider>();
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
                if (catLives > rightBottle.GetComponent<HideNSeekBottles>().catLives)
                {
                    catLives = rightBottle.GetComponent<HideNSeekBottles>().catLives;

                }
            }
            if (gameObject == rightBottle)
            {
                if (catLives > leftBottle.GetComponent<HideNSeekBottles>().catLives)
                {
                    catLives = leftBottle.GetComponent<HideNSeekBottles>().catLives;

                }
            }
            catLives--;
            aS.Play();
            Debug.Log(catLives);
            if (catLives <= 0)
            {
                Debug.Log("CatIsDiededGGEZ");
                vitrinBrain.vitrinState = VitrinStates.Exit;
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
