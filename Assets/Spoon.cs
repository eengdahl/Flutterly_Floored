using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    //For catapult
    public bool sugarFilled, milkFilled, flourFilled;
    // for "Mått"
    public bool sugarInSpoon, milkInSpoon, flourInSpoon;

    public bool isFull;
    public string currentIngredient;
    public float outOfBoundsThreshold;
    public GameObject filling;

    private GameObject player;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool isOutOfBounds;
    private float outOfBoundsTimer;

    [SerializeField] IngredientPickup milk;
    [SerializeField] IngredientPickup suger;
    [SerializeField] IngredientPickup flour;

    //Sounds for pick up
    [SerializeField] AudioClip[] pickUpSounds;
    AudioSource aS;

    //Arrow on catapult
    [SerializeField] GameObject arrowDropOffPoint;
    [SerializeField] PickUpSlev pickUpSlev;

    public void ResetSpoon()
    {
        sugarFilled = false;
        milkFilled = false;
        flourFilled = false;

    }

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        if (isOutOfBounds)
        {
            outOfBoundsTimer += Time.deltaTime;
            if (outOfBoundsTimer > outOfBoundsThreshold)
            {
                player.GetComponentInChildren<PickUpSlev>().OnDrop();
                transform.position = startPosition;
                transform.rotation = startRotation;
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
        if (other.gameObject.name == "CatAMoleBounds")
        {
            isOutOfBounds = false;
            outOfBoundsTimer = 0;
        }
    }

    public void FillSpoon()
    {
        //ljud för pick up
        pickUpSlev.DeactivateArrows();
        int rndIndex = Random.Range(0,3);
        aS.clip = pickUpSounds[rndIndex];
        aS.Play();
        filling.SetActive(true);
        isFull = true;
        arrowDropOffPoint.SetActive(true);
    }

    public void EmptySpoon(bool hasDied)
    {
        if(hasDied == false)
        {

        pickUpSlev.ActivateArrows();
        }
        arrowDropOffPoint.SetActive(false);
        filling.SetActive(false);
        isFull = false;
        if (milkInSpoon == true) milkInSpoon = false;
        else if (sugarInSpoon == true) sugarInSpoon = false;
        else if (flourInSpoon == true) flourInSpoon = false;
    }
 

}
