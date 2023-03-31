using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketScript : MonoBehaviour
{
    
   
    [SerializeField] Transform socketSpot;
    Rigidbody otherRb;
    [SerializeField]Lamp lamp;
    [SerializeField] Cable cable;
    [SerializeField] CharacterJoint cableJoint;
    [SerializeField] PickUpScriptTest pickUpScript;
    [Header("Variables")]
    [SerializeField] float breakForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cable" && cable.canConnect)
        {
            other.gameObject.transform.position = socketSpot.position;          
            other.gameObject.transform.eulerAngles = socketSpot.transform.eulerAngles;//new Vector3(0, 0, -90);
            gameObject.GetComponent<CharacterJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            //other.gameObject.GetComponent<CharacterJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
            otherRb = other.gameObject.GetComponent<Rigidbody>();
            otherRb.constraints = RigidbodyConstraints.FreezeAll;
            lamp.TurnOnLight();
            cable.ChangeLayer();
            cable.canConnect = false;
            Invoke("TurnOnBreakForce",3f);
            pickUpScript.RemoveTarget();
            
        }
    }
    private void Update()
    {

        //Debug.Log(cableJoint.currentForce.magnitude);
        //if (cable.canConnect == false && cableJoint.currentForce.magnitude >)
    }
    void TurnOnBreakForce()
    {
        gameObject.GetComponent<CharacterJoint>().breakForce = breakForce;
    }
    private void OnJointBreak(float breakForce)
    {
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
