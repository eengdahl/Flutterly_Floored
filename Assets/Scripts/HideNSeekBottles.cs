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
    VitrinBrain2 vitrinBrain;
    BoxCollider parentCollider;
    private bool onceLock;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        leftBottle = GameObject.Find("PerfumeButtonLeft");
        rightBottle = GameObject.Find("PerfumeButtonRight");
        buttonReady = true;
        aS = GetComponent<AudioSource>();
        parentCollider = GetComponentInParent<BoxCollider>();
        catInRange = false;
        onceLock = false;
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && buttonReady)
        {
            if (!onceLock)
            {
                vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
                onceLock = true;
            }
            vitrinBrain = FindAnyObjectByType<VitrinBrain2>();
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


        //if (leftBottle.GetComponent<HideNSeekBottles>().catLives !> 1 || rightBottle.GetComponent<HideNSeekBottles>().catLives !> 1)
        //{
        //    return;
        //}
        //if (gameObject == leftBottle)
        //{
        //    if (catLives > rightBottle.GetComponent<HideNSeekBottles>().catLives)
        //    {
        //        catLives = rightBottle.GetComponent<HideNSeekBottles>().catLives;

        //    }
        //}
        //if (gameObject == rightBottle)
        //{
        //    if (catLives > leftBottle.GetComponent<HideNSeekBottles>().catLives)
        //    {
        //        catLives = leftBottle.GetComponent<HideNSeekBottles>().catLives;

        //    }
        //}
        // vitrinBrain.grace = true;
        catLives--;
        aS.Play();

        Debug.Log("CatIsDiededGGEZ");
      //  vitrinBrain.catIsDead = true;


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
