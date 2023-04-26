using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenFanButton : MonoBehaviour
{
    private Animator anim;
    public List<GameObject> otherButtons;
    private bool buttonDown;

    public GameObject kitchenFan;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonPush();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(ButtonReset), 3);
        }
    }

    private void ButtonPush()
    {
        anim.CrossFade("KitchenFanDown", 0, 0);
        kitchenFan.GetComponent<BoxCollider>().enabled = !kitchenFan.GetComponent<BoxCollider>().enabled;
        buttonDown = true;
    }

    private void ButtonReset()
    {
        anim.CrossFade("KitchenFanUp", 0, 0);
        buttonDown = false;
    }
    // Update is called once per frame

}
