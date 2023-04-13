using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Paws : MonoBehaviour
{
    public GameObject pawPrefab;
    public GameObject pawObject;
    public List<GameObject> pawList;
    public RectTransform rectTrans;
    public Transform spawnTransform;
    public Vector3 spawnPosition;
    public float size;
    public float currentLength;

    void Start()
    {
        spawnTransform = transform;
        spawnPosition = transform.position;
        rectTrans = (RectTransform)transform;
        size = rectTrans.localScale.z;
        pawList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLength = (transform.position - spawnPosition).magnitude;
        Debug.Log(currentLength);
        Debug.Log((currentLength + size/pawList.Count * size));

        if(currentLength > size && pawList.Count == 0)
        {
            Debug.Log(size);
            pawObject = Instantiate(pawPrefab, spawnPosition, Quaternion.identity, gameObject.transform);
            pawList.Add(pawObject);
        }
        
        if((currentLength / (pawList.Count * size) > 1))
        {
            int index = pawList.Count - 1;
            Debug.Log(index);
            pawObject = Instantiate(pawPrefab, spawnPosition, Quaternion.identity, pawList[index].gameObject.transform);
            pawList.Add(pawObject);
            
        }
    }

    public void Stretch()
    {
        currentLength = (transform.position - spawnTransform.position).magnitude;
        Debug.Log(currentLength);
    }
}
