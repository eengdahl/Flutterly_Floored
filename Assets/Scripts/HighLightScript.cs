using System.Collections.Generic;
using UnityEngine;

public class HighLightScript : MonoBehaviour
{

    public Transform closestObjectTransform;
    public Material highlightMaterial;

    private List<GameObject> objectsInTrigger = new List<GameObject>();
    private GameObject closestObject;
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rope") || other.gameObject.CompareTag("Handle")|| other.gameObject.CompareTag("HighLightMe"))
        {
            objectsInTrigger.Add(other.gameObject);

            if (!originalMaterials.ContainsKey(other.gameObject))
            {
                originalMaterials.Add(other.gameObject, other.gameObject.GetComponent<Renderer>().material);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rope")|| other.gameObject.CompareTag("Handle") || other.gameObject.CompareTag("HighLightMe"))
        {
            objectsInTrigger.Remove(other.gameObject);

            if (closestObject == other.gameObject)
            {
                closestObject = null;
            }
            RestoreOriginalMaterial(other.gameObject);
        }
    }

    private void Update()
    {
        // Find the closest object to the closestObjectTransform
        float closestDistance = Mathf.Infinity;
        GameObject newClosestObject = null;

        if (objectsInTrigger.Count == 0) return;
        foreach (GameObject obj in objectsInTrigger)
        {
            float distance = Vector3.Distance(closestObjectTransform.position, obj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                newClosestObject = obj;
            }
        }

        // If there is a closest object, change its material to highlightMaterial
        // and change the previous closest object's material back to its original material
        if (newClosestObject != null && newClosestObject != closestObject)
        {
            if (closestObject != null)
            {
                RestoreOriginalMaterial(closestObject);
            }

            closestObject = newClosestObject;
            closestObject.GetComponent<Renderer>().material = highlightMaterial;
        }
        // If there is no closest object, change the previous closest object's material back to its original material
        else if (newClosestObject == null && closestObject != null)
        {
            RestoreOriginalMaterial(closestObject);
            closestObject = null;
        }
    }

    // Restores the original material of an object
    private void RestoreOriginalMaterial(GameObject obj)
    {
        if (originalMaterials.ContainsKey(obj))
        {
            obj.GetComponent<Renderer>().material = originalMaterials[obj];
        }
    }
}