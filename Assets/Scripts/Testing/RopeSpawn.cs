using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    //RopeClimbStart ropeStartScript;
    public List<GameObject> listOfTransforms;

    [SerializeField]
    [Range(1, 1000)]
    int length = 1;

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    GameObject ropeStart, ropeEnd, partPrefab, parentObject;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;

    void Update()
    {
        if (reset)
        {
            foreach (GameObject temporaryGameObject in GameObject.FindGameObjectsWithTag("Rope"))
            {
                Destroy(temporaryGameObject);
            }
        }
        if (spawn)
        {
            Spawn();
            spawn = false;
        }
    }
    public void Spawn()
    {
        int count = (int)(length / partDistance);

        for (int i = 0; i < count; i++)
        {
            GameObject temporaryGameObject;
            temporaryGameObject = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y + partDistance * (i + 1), transform.position.z), Quaternion.identity, parentObject.transform);
            temporaryGameObject.transform.eulerAngles = new Vector3(180, 0, 0);
            temporaryGameObject.name = parentObject.transform.childCount.ToString();
            
            listOfTransforms.Add(temporaryGameObject);

            if (i == count-1)
            {
                //ropeStartScript.transformSpots = listOfTransforms;
            }


            if (i == 0)
            {
             
                Destroy(temporaryGameObject.GetComponent<CharacterJoint>());
                if (snapFirst)
                {
                    temporaryGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    //parentObject.transform.Find("1").transform.position = ropeEnd.transform.position;
                }
            }
            else
            {
                temporaryGameObject.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>(); 
            }
            
            
        }
        if (snapLast)
        {
            parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            //get at the rope end
            
            Debug.Log("Hello my name is" + parentObject.transform.Find((parentObject.transform.childCount).ToString()));
        }

        //Set start and end position
        parentObject.transform.Find((parentObject.transform.childCount).ToString()).transform.position = ropeStart.transform.position;
        parentObject.transform.Find("1").transform.position = ropeEnd.transform.position;
    }
}
