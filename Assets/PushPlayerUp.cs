using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayerUp : MonoBehaviour
{
    public Transform lowerRayOrigin;        // Transform representing the origin point of the front raycast
    public Transform higherRayOrigin;       // Transform representing the origin point of the higher raycast
    public float raycastDistance = 1.0f;    // Distance to cast the raycasts
    public LayerMask groundLayer;           // Layer mask to detect the "Ground" layer

    PlayerJump jumpScript;
    BirdCableMovement climbingScript;

    private bool isGrounded;                // Flag to track if the player is grounded

    private void Start()
    {
        jumpScript = GetComponent<PlayerJump>();
        climbingScript = GetComponent<BirdCableMovement>();
    }

    void Update()
    {
        if (jumpScript.isGrounded) return;
        if (climbingScript.isClimbing) return;
        // Perform raycasts
        RaycastHit frontHit;
        bool frontRaycast = Physics.Raycast(lowerRayOrigin.position, lowerRayOrigin.forward, out frontHit, raycastDistance, groundLayer);

        RaycastHit higherHit;
        bool higherRaycast = Physics.Raycast(higherRayOrigin.position, higherRayOrigin.forward, out higherHit, raycastDistance + 0.1f, groundLayer);

        // Check the results of the raycasts
        if (frontRaycast)
        {
            if (!higherRaycast || higherHit.collider.gameObject != frontHit.collider.gameObject)
            {
                // Teleport the character to the level of the higher raycast
                Vector3 newPosition = transform.position;
                newPosition.y = higherRayOrigin.transform.position.y + 0.4f;
                transform.position = newPosition;
                isGrounded = true;
                //Debug.Log("Should be higher up now");
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }

        // Debug visualization of raycasts
        Debug.DrawRay(lowerRayOrigin.position, lowerRayOrigin.forward * raycastDistance, frontRaycast ? Color.green : Color.red);
        Debug.DrawRay(higherRayOrigin.position, higherRayOrigin.forward * raycastDistance, higherRaycast ? Color.green : Color.red);
    }
}