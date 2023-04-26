using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpScriptTest : MonoBehaviour
{

    //Materials
    [SerializeField] Material TargetMaterial;
    private Material normalMaterial;


    [SerializeField]GameObject targetTransform;

    List<GameObject> items;
    [SerializeField] GameObject thingToPull;
    private Dictionary<GameObject, float> distances = new Dictionary<GameObject, float>();

    float force = 1000;

    private void Start()
    {
        items = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && other.gameObject.name == "PickUpBox")
        {
            items.Add(other.gameObject);
        }
        if (other.gameObject != null && other.gameObject.tag == "Rope")
        {
            items.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PickUpBox" || other.gameObject.tag == "Rope")
        {
            items.Remove(other.gameObject);
        }
    }

    private void Update()
    {


        if (thingToPull != null)
        {
            Vector3 Distance = targetTransform.transform.position - thingToPull.transform.position; // line from pickup to player
            float dist = Distance.magnitude;
            Vector3 pullDir = Distance.normalized; // short blue arrow from crate to player
            if (dist > 2f)
            {
                //RemoveTarget if to far away

                RemoveTarget();
            }
            else if (dist > 0f) //If to close
            {
                float pullF = force; //10 test gravity
                //make pullforce depending on distance
                float pullForDist = (dist - 0.5f) / 2.0f;
                if (pullForDist > 20)
                {
                    pullForDist = 20;
                }

                pullF += pullForDist;
                thingToPull.GetComponent<Rigidbody>().velocity += pullDir * (pullF * Time.deltaTime);
                // Check if the game object has reached the target transform point
                if (dist < 0.1f) 
                {
                    thingToPull.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }

        }
    }
    void CalculateDistances()
    {
        foreach (GameObject item in items)
        {
            float distance = Vector3.Distance(item.transform.position, targetTransform.transform.position);
            distances[item] = distance;
        }
    }
    void FindShortestDistance()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject closestItem = null;

        foreach (GameObject item in items)
        {
            if (distances[item] < shortestDistance)
            {
                shortestDistance = distances[item];
                closestItem = item;
            }
        }
        //Set thing to pull to closest  item and change colour
        thingToPull = closestItem;
        //SetMaterialTarget();

        //If rope change force
        //if (thingToPull.tag == "Rope")
        //{
        //    force = 1000;
        //}
    }

    void SetMaterialTarget()
    {
        normalMaterial = thingToPull.GetComponent<MeshRenderer>().material;
        thingToPull.GetComponent<MeshRenderer>().material = TargetMaterial;
    }
    void ReturnMaterial()
    {
        if (thingToPull != null)
        {

            thingToPull.GetComponent<MeshRenderer>().material = normalMaterial;
        }
    }
    public void RemoveTarget()
    {
        if (thingToPull != null)
        {
            //ReturnMaterial();
            thingToPull = null;

        }
    }

    public void DragObject(InputAction.CallbackContext Drag)
    {
        if (Drag.started)
        {
            if (items.Count > 0)
            {
                CalculateDistances();
                FindShortestDistance();
            }
        }
        if (Drag.canceled)
        {
            RemoveTarget();
        }
    }

}
