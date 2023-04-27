using UnityEngine;

public class TurnOnOffDistance : MonoBehaviour
{
    public Transform playerTransform;
    public float activationDistance = 10.0f;
    public GameObject onlyMeshObject;
    public GameObject physicsObject;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= activationDistance)
        {
            physicsObject.SetActive(true);
            onlyMeshObject.SetActive(false);
        }
        else
        {
            physicsObject.SetActive(false);
            onlyMeshObject.SetActive(true);
        }
    }
}