using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketScript : MonoBehaviour
{
    [SerializeField] Transform socketSpot;
    Rigidbody otherRb;
    Lamp lamp;
    Cable cable;
    RadioButton radio;
    Fan fan;
    [SerializeField] CharacterJoint cableJoint;
    [SerializeField] PickUpScriptTest pickUpScript;
    [Header("Variables")]
    [SerializeField] float breakForce;
    bool socketOccupied;
    public AudioManager audioManager;
    public float transitionDuration = 1f; // Duration of the transition in seconds
    private void Start()
    {
        socketOccupied = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cable" && socketOccupied == false)
        {

            //Get the lamp that that cable is connected to
            if (other.gameObject.GetComponent<Cable>().lamp != null)
            {
                lamp = other.gameObject.GetComponent<Cable>().lamp;
            }
            //Get the radio that cable is connected to
            if (other.gameObject.GetComponent<Cable>().radio != null)
            {
                radio = other.gameObject.GetComponent<Cable>().radio;
            }

            if(other.gameObject.GetComponent<Cable>().fan != null)
            {
                fan = other.gameObject.GetComponent<Cable>().fan;
            }

            cable = other.gameObject.GetComponent<Cable>();

            if (cable.canConnect)
            {
                //Remove the players target
                pickUpScript.RemoveTarget();

                //Push the socket into the walloposition
                //other.gameObject.transform.position = socketSpot.position;
                //other.gameObject.transform.eulerAngles = socketSpot.transform.eulerAngles;//new Vector3(0, 0, -90);
                //gameObject.GetComponent<CharacterJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
                TransitionObjectToSocket(other.gameObject, socketSpot);


                //Freeze its position
                otherRb = other.gameObject.GetComponent<Rigidbody>();
                otherRb.constraints = RigidbodyConstraints.FreezeAll;

                //Turn on the light
                if (lamp != null)
                {
                    lamp.TurnOnLight();
                    cable.ChangeLayer();
                    cable.canConnect = false;
                }
                //Turn on radio
                if(radio != null)
                {
                    radio.gotElectricity = true;
                }
                //Turn on fan
                if(fan != null)
                {
                    //Debug.Log("Fan connected");
                    fan.TurnOn();
                }

                //Turn on break force so it can be removed from socket 3seconds after it has been set in
                Invoke(nameof(TurnOnBreakForce), 3f);

                

                //Set socket occupied
                socketOccupied = true;

            }

        }
    }

    void TurnOnBreakForce()
    {
        gameObject.GetComponent<CharacterJoint>().breakForce = breakForce;
    }
    private void OnJointBreak(float breakForce)
    {
        socketOccupied = false;
        cable.canConnect = true;
        CreateNewJoint();
        otherRb.constraints = RigidbodyConstraints.None;

        //Get the lamp that that cable is connected to
        if (lamp != null)
        {
            lamp.TurnOffLight();
            lamp = null;
        }
        //Get the radio that cable is connected to
        if (radio != null)
        {
            radio.isPlaying = false;
            radio.gotElectricity = false;
            radio = null;
            audioManager.ResumeMainMusic();
        }
        if(fan != null)
        {
            PlayerWind playerWind = FindObjectOfType<PlayerWind>();
            playerWind.LeaveWindArea();
            fan.TurnOff();
            fan = null;
        }

    }

    void CreateNewJoint()
    {
        gameObject.AddComponent<CharacterJoint>();
        gameObject.GetComponent<CharacterJoint>().anchor = new Vector3(0, 0.35f, 0);
    }

    

    public void TransitionObjectToSocket(GameObject other, Transform socketSpot)
    {
        StartCoroutine(TransitionCoroutine(other, socketSpot));
    }

    private IEnumerator TransitionCoroutine(GameObject other, Transform socketSpot)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = other.transform.position;
        Quaternion initialRotation = other.transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // Interpolate position and rotation
            other.transform.position = Vector3.Lerp(initialPosition, socketSpot.position, t);
            other.transform.rotation = Quaternion.Lerp(initialRotation, socketSpot.rotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and rotation
        other.transform.position = socketSpot.position;
        other.transform.rotation = socketSpot.rotation;

        // Connect rigidbody
        gameObject.GetComponent<CharacterJoint>().connectedBody = other.GetComponent<Rigidbody>();
    }
}
