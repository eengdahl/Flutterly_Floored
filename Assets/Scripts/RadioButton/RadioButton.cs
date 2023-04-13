using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadioButton : MonoBehaviour
{
    [SerializeField] public Transform buttonPart;
    [SerializeField] Transform buttonLowerLimit;
    [SerializeField] Transform buttonUpperLimit;
    public float threshHold;
    public float force = 10;
    private float upperLowerDiff;
    public bool isPressed;
    private bool prevPressedState;
    [SerializeField] public AudioSource pressedSound;
    [SerializeField] AudioSource releasedSound;
    AudioSource aS;
    //bool is playing
    public bool isPlaying;
    public bool gotElectricity;

    public AudioManager audioManager;



    void Start()
    {
        gotElectricity = false;
        aS = GetComponent<AudioSource>();
        isPlaying = false;
        audioManager = FindObjectOfType<AudioManager>();
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
        if (!gotElectricity)
        {
         
            aS.Stop();

           
        }

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

    private void Pressed()
    {
        prevPressedState = isPressed;
        //pressedSound.pitch = 1;
        //presseddSound.Play();

        //TurnMusic on or off
        if (!isPlaying)
        {
            Debug.Log("IS PLAYING MUSIC");
            aS.Play();
            audioManager.TurnOfMainMusic();
            isPlaying = true;
        }
        else if (isPlaying)
        {
            aS.Stop();
            audioManager.ResumeMainMusic();
            isPlaying = false;
        }
    }

    private void Released()
    {
        prevPressedState = isPressed;
        //releasedSound.pitch = Random.Range(1.1f, 1.2f);
        //releasedSound.Play();

    }
}
