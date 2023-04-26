using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FanButton : MonoBehaviour
{
    [SerializeField] public Transform buttonPart;
    [SerializeField] Transform buttonLowerLimit;
    [SerializeField] Transform buttonUpperLimit;
    public float threshHold;
    public float force = 10;
    private float upperLowerDiff;
    public bool isPressed;
    private bool prevPressedState;

    //bool is playing
    [SerializeField] bool isStrButton;
    public bool gotElectricity;
    public bool buttonOn;
    [SerializeField] Fan fanScript;



    void Start()
    {

        buttonOn = false;
        gotElectricity = false;

        Physics.IgnoreCollision(GetComponent<Collider>(), buttonPart.GetComponent<Collider>());
        if (transform.eulerAngles != Vector3.zero)
        {
            //Save the angle of the button
            Vector3 savedAngle = transform.eulerAngles;
            //Change angle to zero
            transform.eulerAngles = Vector3.zero;
            //calculate angle diff
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            //Set it back
            transform.eulerAngles = savedAngle;
        }
        else
        {
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
        }
    }


    void Update()
    {


        buttonPart.transform.localPosition = new Vector3(0, buttonPart.transform.localPosition.y, 0); //do this in unityeditor instead?
        buttonPart.transform.localEulerAngles = Vector3.zero;

        //Bring button back to  Upper limit if going to far
        if (buttonPart.localPosition.y >= 0)
        {
            buttonPart.transform.position = buttonUpperLimit.position;
        }
        else
        {
            buttonPart.GetComponent<Rigidbody>().AddForce(buttonPart.transform.up * force * Time.fixedDeltaTime);
        }
        if (fanScript.on)
        {
            //Bring button back to LowerLimit if going to far
            if (buttonPart.localPosition.y <= buttonLowerLimit.localPosition.y)
            {
                buttonPart.transform.position = buttonLowerLimit.position;
                buttonPart.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            //threshold check for turning on and off
            if (Vector3.Distance(buttonPart.position, buttonLowerLimit.position) < upperLowerDiff * threshHold)
            {
                Debug.Log("Should be pressed");
                isPressed = true;
            }
            else
            {
                isPressed = false;
            }

            if (isPressed && prevPressedState != isPressed)
            {
                Pressed();
            }
            if (!isPressed && prevPressedState != isPressed)
            {
                Released();
            }

        }

    }

    private void Pressed()
    {
        prevPressedState = isPressed;
        if (!isStrButton)
        {
            if (!buttonOn)
            {
                buttonOn = true;
            }
            else if (buttonOn)
            {
                buttonOn = false;
            }
        }
        else if (isStrButton)
        {
            fanScript.SwitchWindStr();
        }

    }

    private void Released()
    {
        prevPressedState = isPressed;
    }
}
