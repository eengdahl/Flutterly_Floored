using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketScript : MonoBehaviour
{
    [SerializeField] Transform socketSpot;
    Rigidbody otherRb;
    Lamp lamp; 
    Cable cable;
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
        if(other.gameObject.tag == "Cable" && socketOccupied == false)
        {
           
            //Get the lamp that that cable is connected to
            lamp = other.gameObject.GetComponent<Cable>().lamp;
            cable = other.gameObject.GetComponent<Cable>();
            if (cable.canConnect)
            {
            //Push the socket into the walloposition
            other.gameObject.transform.position = socketSpot.position;          
            other.gameObject.transform.eulerAngles = socketSpot.transform.eulerAngles;//new Vector3(0, 0, -90);
            gameObject.GetComponent<CharacterJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
           

            //Freeze its position
            otherRb = other.gameObject.GetComponent<Rigidbody>();
            otherRb.constraints = RigidbodyConstraints.FreezeAll;

            //Turn on the light
            lamp.TurnOnLight();
            cable.ChangeLayer();
            cable.canConnect = false;

            //Turn on break force so it can be removed from socket 3seconds after it has been set in
            Invoke("TurnOnBreakForce",3f);

            //Remove the players target
            pickUpScript.RemoveTarget();

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
        lamp.TurnOffLight();
        CreateNewJoint();
        otherRb.constraints = RigidbodyConstraints.None;

    }

    void CreateNewJoint()
    {
        gameObject.AddComponent<CharacterJoint>();
        gameObject.GetComponent<CharacterJoint>().anchor = new Vector3(0, 0.35f, 0);
    }
}
