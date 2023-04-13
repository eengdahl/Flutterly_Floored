using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        MainMusic();
    }


    public void TurnOfMainMusic()
    {
        aS.Stop();
    }
    public void ResumeMainMusic()
    {
        aS.loop = true;
        aS.Play();
    }
    public void MainMusic()
    {
        aS.loop = true;
        aS.Play();
    }


}
