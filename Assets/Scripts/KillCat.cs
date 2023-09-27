using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCat : MonoBehaviour
{
    public GameObject puffPrefab;
    public Animator cataMoleAnimator;
    public GameObject catamoleDeathCube;
    public GameObject catamoleCamSwap;
    AudioManager audiohandeler;

    private GameObject puffObject;

    // Start is called before the first frame update
    void Start()
    {
        audiohandeler = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("CatCatAMole"))
        {
            puffObject = Instantiate(puffPrefab, transform.position, Quaternion.identity);
            puffObject.transform.localScale = puffObject.transform.localScale * 2;


            StartCoroutine(DestroyPuff());
            //Debug.Log("hit the motherfukking cat!!");
            //collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator DestroyPuff()
    {
        cataMoleAnimator.SetTrigger("DMKCat");
        yield return new WaitForSeconds(2);
        catamoleCamSwap.SetActive(false);
        catamoleDeathCube.SetActive(false);
        audiohandeler.ResumeMainMusic();
        Destroy(puffObject);
    }
}
