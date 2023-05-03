using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float resetTime;
    public float resetSpeed;

    private bool isResettingPosition;
    private Vector3 startVector;
    private Vector3[] startPosition;
    private Vector3 moveFrom;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        water = GameObject.FindGameObjectWithTag("Water");
        startPosition = new Vector3[floatingPoints.Length];
        startVector = transform.position;
        for(int i = 0; i < floatingPoints.Length; i++)
        {
            startPosition[i] = floatingPoints[i].transform.position;
        }
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
                else if (pointsUnderwater == 0)
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

        if (Input.GetKeyDown(KeyCode.T))
        {
                TriggerReset();
        }

        if (isResettingPosition)
        {
            ResetPosition();
        }
    }

    private void CalculateDepth(Transform floater)
    {
        //Debug.Log(water.GetComponent<Water>().GetAdvancedWaterHeight(floater.position));
        depth = floater.position.y - water.GetComponent<Water>().GetSimpleWaterHeight();
        //depth = floater.position.y - water.GetComponent<Water>().GetAdvancedWaterHeight(floater.position);
    }

    private void TriggerReset()
    {
        moveFrom = transform.position;
        isResettingPosition = true;

        water.GetComponent<Water>().isDraining = true;
    }

    private void ResetPosition()
    {
        Debug.Log((startVector - transform.position).magnitude);
        if ((startVector - transform.position).magnitude > 0.1f)
        {
            Debug.Log("Running");
            float tick = resetSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y,transform.position.z), new Vector3(startVector.x, transform.position.y, startVector.z), resetSpeed/1000);
        }
        else
            isResettingPosition = false;
    }

   
}
