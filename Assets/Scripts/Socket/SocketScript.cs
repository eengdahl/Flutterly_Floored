using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketScript : MonoBehaviour
{
    [SerializeField] Transform socketSpot;
    Rigidbody rb;
    [SerializeField]Lamp lamp;
    [SerializeField] Cable cable;
    [SerializeField] CharacterJoint cableJoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cable" && cable.canConnect)
        {       
            other.gameObject.transform.position = socketSpot.position;
            other.gameObject.transform.eulerAngles = socketSpot.transform.eulerAngles;//new Vector3(0, 0, -90);
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            lamp.TurnOnLight();
            cable.ChangeLayer();
            cable.canConnect = false;
        }
    }
    private void Update()
    {
        //Debug.Log(cableJoint.currentForce.magnitude);
        //if (cable.canConnect == false && cableJoint.currentForce.magnitude >)
    }
}
