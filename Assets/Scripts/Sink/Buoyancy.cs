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
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        water = GameObject.FindGameObjectWithTag("Water");

        startPosition = transform.position;
        startRotation = transform.rotation;
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
        SimpleWater simpleWater = water.GetComponent<SimpleWater>();
        //Debug.Log(water.GetComponent<Water>().GetAdvancedWaterHeight(floater.position));
        depth = floater.position.y - (simpleWater.transform.position.y + simpleWater.GetWaterHeight(floater.position, 0.69f));
        //depth = floater.position.y - water.GetComponent<Water>().GetAdvancedWaterHeight(floater.position);
    }

    private void TriggerReset()
    {
        isResettingPosition = true;

        water.GetComponent<Water>().isDraining = true;
    }

    //public IEnumerator DelayedReset()
    //{
    //    yield return new WaitForSeconds(2);
    //    ResetPosition();
    //}

    public void ResetPosition()
    {
        transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
        Vector2 tmpStartVector = new Vector2(startPosition.x, startPosition.z);
        Vector2 tmpCurrentVextor = new Vector2(transform.position.x, transform.position.z);
        transform.rotation = startRotation;

        if((tmpStartVector - tmpCurrentVextor).magnitude < 0.1f)
        {
            isResettingPosition = false;
        }
        //Debug.Log((startVector - transform.position).magnitude);
        //if ((startPosition - transform.position).magnitude > 0.1f)
        //{
        //    Debug.Log("Running");
        //    float tick = resetSpeed * Time.deltaTime;
        //    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y,transform.position.z), new Vector3(startPosition.x, transform.position.y, startPosition.z), resetSpeed/1000);
        //}
        //else
        //    isResettingPosition = false;
    }
}
