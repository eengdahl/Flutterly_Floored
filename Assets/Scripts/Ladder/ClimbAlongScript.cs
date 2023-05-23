using System.Collections.Generic;
using UnityEngine;

public class ClimbAlongScript : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public bool rotationStartLocked;
    public Vector3 startRotation;
    public bool isJungle;
    Rigidbody rb;
    public bool canFall;
    public MeshCollider meshCollider;
    public bool isSmallClimbing;
    public ClimbAlongScript otherClimbAlong;
    public int otherClimbingIndex;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    private void OnDrawGizmos()
    {
        // Draw the cable in the scene view using Gizmos
        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }
    }

    public void FallToGround()
    {
        rb.isKinematic = false;
        meshCollider.enabled = true;
        gameObject.SetActive(false);
    }
}
