using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenFanButton : MonoBehaviour
{
    private Animator anim;
    public List<GameObject> otherButtons;
    public bool buttonDown;
    [SerializeField] private bool offButton;
    public float fanMultiplier;



    public GameObject kitchenFan;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (offButton)
        {
            anim.CrossFade("KitchenFanDown", 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !buttonDown)
        {
            ButtonPush();
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<KitchenFanButton>().ButtonReset();
            }
        }
    }

    private void ButtonPush()
    {
        if (offButton)
        {
            anim.CrossFade("KitchenFanDown", 0, 0);
            kitchenFan.GetComponent<BoxCollider>().enabled = false;
            buttonDown = true;
        }
        else
        {
            anim.CrossFade("KitchenFanDown", 0, 0);
            kitchenFan.GetComponent<BoxCollider>().enabled = true;
            buttonDown = true;
        }
    }

    private void ButtonReset()
    {
        if (buttonDown)
        {
            anim.CrossFade("KitchenFanButtonUp", 0, 0);
            buttonDown = false;
        }
    }
    // Update is called once per frame

}
