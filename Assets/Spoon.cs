using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    public bool sugarFilled, milkFilled, flourFilled;
    public bool isFull;
    public string currentIngredient;
    public float outOfBoundsThreshold;
    public GameObject filling;

    private GameObject player;
    private Vector3 startPosition;
    private bool isOutOfBounds;
    private float outOfBoundsTimer;

    public void ResetSpoon()
    {
        sugarFilled = false;
        milkFilled = false;
        flourFilled = false;
        
    }

    private void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        if(isOutOfBounds)
        {
            outOfBoundsTimer += Time.deltaTime;
            if(outOfBoundsTimer > outOfBoundsThreshold)
            {
                player.GetComponentInChildren<PickUpSlev>().OnDrop();
                transform.position = startPosition;
                isOutOfBounds = false;
                outOfBoundsTimer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "CatAMoleBounds")
        {
            isOutOfBounds = true;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "CatAMoleBounds")
        {
            isOutOfBounds = false;
            outOfBoundsTimer = 0;
        }
    }

    public void FillSpoon()
    {
        filling.SetActive(true);
        isFull = true;
    }

    public void EmptySpoon()
    {
        filling.SetActive(false);
        isFull = false;
    }

}
