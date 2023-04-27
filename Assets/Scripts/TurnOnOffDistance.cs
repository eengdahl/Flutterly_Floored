using UnityEngine;

public class TurnOnOffDistance : MonoBehaviour
{
    public Transform playerTransform;
    float activationDistance = 10f;
    public GameObject onlyMeshObject;
    public GameObject physicsObject;
    InJungleGymChecker gymChecker;

    private void Start()
    {
        gymChecker = FindAnyObjectByType<InJungleGymChecker>();

        CheckDistance();
    }
    private void Update()
    {
        if (!gymChecker.inJungle) return;
        CheckDistance();
    }

    void Activate()
    {
        physicsObject.SetActive(true);
        onlyMeshObject.SetActive(false);
    }
    void DeActivate()
    {
        physicsObject.SetActive(false);
        onlyMeshObject.SetActive(true);
    }
    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= activationDistance)
        {
            Activate();
        }
        else
        {
            DeActivate();
        }
    }
}