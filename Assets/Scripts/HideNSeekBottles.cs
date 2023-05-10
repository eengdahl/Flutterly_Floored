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
    public GameObject blocker;
    public Animator anim;
    VitrinBrain2 vitrinBrain;
    BoxCollider parentCollider;
    private bool onceLock;
    public AudioClip cat;
    public AudioClip spray;
    public GameObject sprayPS;

    private void Start()
    {
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

            aS.PlayOneShot(spray);
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
        sprayPS.SetActive(true);
        Debug.Log("CatIsDiededGGEZ");
        vitrinBrain.activeState = VitrinState.Exit;
        vitrinBrain.catIsDead = true;
        Invoke(nameof(DestroyBlocker), 1);

        //button push
        buttonReady = false;
        this.gameObject.SetActive(false);
        //  anim.CrossFade("PerfumeButtonPush", 0, 0);
    }

    private IEnumerator ButtonReset()
    {
        yield return new WaitForSeconds(10f);
        anim.CrossFade("PerfumeButtonInflate", 0, 0);
        buttonReady = true;
        sprayPS.SetActive(false);
    }

    void DestroyBlocker()
    {
        Destroy(blocker);

    }




}
