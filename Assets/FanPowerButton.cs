using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPowerButton : MonoBehaviour
{

    public bool buttonPushed;
    bool canBePushed;
    Animator animator;
    [SerializeField]Fan fanScript;

    [SerializeField] bool isSpeedButton;
    [SerializeField] bool isRotationButton;
    [SerializeField] bool isPowerButton;
    
    private void Start()
    {       
        animator = GetComponent<Animator>();
        canBePushed = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canBePushed)
            {
                animator.SetBool("ButtonPushed", true);

                ButtonPushed();
                canBePushed = false;
            }
        }
    }



    public void CanBePushed()
    {
        canBePushed = true;
    }
    public void resetBool()
    {
        CanBePushed();
        animator.SetBool("ButtonPushed", false);
    }
    
    public void ButtonPushed()
    {
        if (!buttonPushed)
        {
            buttonPushed = true;          
            if (isRotationButton)
            {
                fanScript.shouldRotate = true;
            }
            else if (isSpeedButton)
            {
                fanScript.SwitchWindStr();
            }
            else if (isPowerButton)
            {
                fanScript.aS.Stop();
                fanScript.on = false;
                

            }
        }
        else
        {
            buttonPushed = false;

            if (isRotationButton)
            {
                fanScript.shouldRotate = false;
            }
            else if (isSpeedButton)
            {
                fanScript.SwitchWindStr();
            }
            else if (isPowerButton)
            {
                fanScript.aS.Play();
                fanScript.on = true;

            }
        }
    }
}
