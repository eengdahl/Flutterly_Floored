using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTwo : MonoBehaviour
{
    public GameObject MainCamera;
    // public GameObject secondCamera;
    public Cinemachine.CinemachineFreeLook c_VirtualCamera;
    public Transform branch;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            c_VirtualCamera.m_LookAt = branch.transform;
            // c_VirtualCamera.m_Follow = other.transform;

            //  secondCamera.transform.RotateAround(transform.position, transform.up, 180f);

        }
    }



    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            c_VirtualCamera.m_LookAt = other.transform;

        }
    }
}