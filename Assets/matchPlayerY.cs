using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class matchPlayerY : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject branch;
    private float distanceBetween;

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceBetween = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        this.transform.position = new Vector3((transform.position.x), player.transform.position.y, transform.position.z);

    }
}
