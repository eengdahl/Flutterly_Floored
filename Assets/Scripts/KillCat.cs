using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillCat : MonoBehaviour
{
    public GameObject puffPrefab;
    public Animator cataMoleAnimator;
    AudioManager audiohandeler;

    private GameObject puffObject;

    // Start is called before the first frame update
    void Start()
    {
        audiohandeler = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("CatCatAMole"))
        {
            puffObject = Instantiate(puffPrefab, transform.position, Quaternion.identity);
            puffObject.transform.localScale = puffObject.transform.localScale * 2;


            StartCoroutine(DestroyPuff());
            Debug.Log("hit the motherfukking cat!!");
            //collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator DestroyPuff()
    {
        cataMoleAnimator.SetTrigger("DMKCat");
        yield return new WaitForSeconds(2);
        audiohandeler.ResumeMainMusic();
        Destroy(puffObject);
    }
}
