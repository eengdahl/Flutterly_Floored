using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeOfDeath : MonoBehaviour
{

    public VitrinBrain brain;
    public GameObject visionCone;
    public float distance;
    public LayerMask player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        //Debug.Log(other.gameObject.name);

        if(other.CompareTag("Player") && brain.isGazing)
        {
            RaycastHit hit;
            Physics.Raycast(visionCone.transform.position, other.transform.position - visionCone.transform.position, out hit,distance, player);
            //Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
            }

        }
    }
}
