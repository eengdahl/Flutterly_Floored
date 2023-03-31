using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeClimbStart : MonoBehaviour
{
    GameObject player;
    public List<GameObject> transformSpots;
    bool shouldMove = false;

    [SerializeField]
    GameObject footOne;
    [SerializeField]
    GameObject foothTwo;

    //Povosoriskt
    [SerializeField]
    float speed = 0.1f;
    private int current = 0;
    private Vector3 offset = new Vector3(0, 0.3f, 0);
    float distanceToNext;
    // Angular speed in radians per sec.
    public float speedRotation = 1.0f;

    void Update()
    {
        if (shouldMove == false) return;


            //Rotation of bird
        
            // The step size is equal to speed times frame time.
            var step = speedRotation * Time.deltaTime;
        

        //Provosorisk movement
        distanceToNext = Vector3.Distance(player.transform.position, transformSpots[current].transform.position + offset);
        
        if (player.transform.position != transformSpots[current].transform.position + offset)
        {
            //Dela upp sa att move to x z är enligt speed, medans y ar exakt samma helatiden
            Vector3 pos = Vector3.MoveTowards(player.transform.position, transformSpots[current].transform.position + offset, speed * Time.deltaTime);
            player.GetComponent<Rigidbody>().MovePosition(pos);
            //player.transform.position = new Vector3(pos.x, transformSpots[current].transform.position.y, pos.z) + offset;
        }

        if (distanceToNext < 0.1f)
        {

            if (current < transformSpots.Count-1)
            {
                //Go to next one 
                current++;
                //Set weight
                transformSpots[current].GetComponent<Rigidbody>().mass = 200;
                //Weight behind
                if (current > 0)
                {
                    transformSpots[current - 1].GetComponent<Rigidbody>().mass = 100;
                }
                if (current > 1)
                {
                    transformSpots[current - 2].GetComponent<Rigidbody>().mass = 20;
                }
                //Weight infront
                if (current < transformSpots.Count - 1)
                {
                    transformSpots[current + 1].GetComponent<Rigidbody>().mass = 100;
                }
                if (current < transformSpots.Count - 2)
                {
                    transformSpots[current + 2].GetComponent<Rigidbody>().mass = 20;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            shouldMove = true;
            player.transform.position = transformSpots[current].transform.position + offset;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
