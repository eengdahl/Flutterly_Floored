using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdOfDoom : MonoBehaviour
{
    public AudioSource doom;
    // Start is called before the first frame update
    void Start()
    {
        doom.Play();
    }
}
