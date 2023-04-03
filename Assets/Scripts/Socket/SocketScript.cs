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
                other.gameObject.transform.position = socketSpot.position;
                other.gameObject.transform.eulerAngles = socketSpot.transform.eulerAngles;//new Vector3(0, 0, -90);
                gameObject.GetComponent<CharacterJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();


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
                    Debug.Log("Fan connected");
                    fan.TurnOn();
                }

                //Turn on break force so it can be removed from socket 3seconds after it has been set in
                Invoke("TurnOnBreakForce", 3f);

                

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
        }
        if(fan != null)
        {
            fan.TurnOff();
            fan = null;
        }

    }

    void CreateNewJoint()
    {
        gameObject.AddComponent<CharacterJoint>();
        gameObject.GetComponent<CharacterJoint>().anchor = new Vector3(0, 0.35f, 0);
    }
}
