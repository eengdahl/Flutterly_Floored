using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<MovingLeaves>().MoveLeaf();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<MovingLeaves>().MoveLeaf();
        }
    }
}
