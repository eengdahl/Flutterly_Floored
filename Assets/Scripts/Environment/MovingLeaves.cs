using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeaves : MonoBehaviour
{
    public Animation moveLeaves;

    // Start is called before the first frame update
    void Start()
    {
        moveLeaves = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveLeaf()
    {
        {
            moveLeaves.Play();
        }
    }
}
