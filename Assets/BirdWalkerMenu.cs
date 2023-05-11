using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdWalkerMenu : MonoBehaviour
{
    public bool startMoveing;
    public List<Transform> positions;
    int currentPosition;


    private void Start()
    {
        currentPosition = 0;
        startMoveing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startMoveing)
        {
            if (Vector3.Distance(transform.position, positions[currentPosition].position) < 0.01f)
            {
               
                currentPosition++;
                if (currentPosition > positions.Count-1)
                {
                    currentPosition = 0;
                    return;
                }
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position, positions[currentPosition].position, 20 * Time.deltaTime);


        }
    }
}
