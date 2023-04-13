using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Buoyancy : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private GameObject water;
    private float depth;
    private Vector3 decelerationVector;
    private float pointsUnderwater;


    public Transform[] floatingPoints;

    public float buoyancyFactor;
    public float gravity;
    public float waterDeceleration;
    public float airDeceleration;
    public bool isBelowSurface;
    public float surfaceOffset;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        water = GameObject.FindGameObjectWithTag("Water");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pointsUnderwater = 0;
        foreach (Transform floater in floatingPoints)
        {
            CalculateDepth(floater);

            if (depth < 0f + surfaceOffset)
            {
                rb.AddForceAtPosition(Vector3.up * buoyancyFactor * Mathf.Abs(depth + surfaceOffset) - decelerationVector, floater.position, ForceMode.Force);
                pointsUnderwater++;
                if (pointsUnderwater != 0)
                {
                    isBelowSurface = true;
                    decelerationVector = rb.velocity * waterDeceleration;
                }
                else if(pointsUnderwater == 0)
                {
                    isBelowSurface = false;
                    decelerationVector = rb.velocity * airDeceleration;
                }



            }
            else
            {
                //decelerationVector = rb.velocity * airDeceleration;
                //rb.AddForceAtPosition(Vector3.down * Mathf.Abs(depth) - decelerationVector, transform.position, ForceMode.Force);
            }
        }
    }

    private void CalculateDepth(Transform floater)
    {
        //depth = floater.position.y - water.GetComponent<Water>().GetHeigthAtPosition(floater.position);
        depth = floater.position.y - water.GetComponent<Water>().GetSimpleWaterHeight();
    }

}
