using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppScript : MonoBehaviour
{
    AudioSource aS;
    public AudioClip step;
    public AudioClip sprint;
    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void PlayStep()
    {
        aS.pitch = Random.Range(2f, 3f);
        aS.PlayOneShot(step);

    }

    public void StopStepSound()
    {
        aS.Stop();
    }

    public void PlaySprint()
    {
        aS.pitch = Random.Range(2f, 3f);
        aS.PlayOneShot(sprint);
    }
}
