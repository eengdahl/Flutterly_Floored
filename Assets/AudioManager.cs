using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource aS;
    public List<AudioClip> clipList;
    private int nrInList = 1;
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
        aS.clip = clipList[nrInList - 1];
        aS.Play();
    }
    public void MainMusic()
    {
        nrInList++;
        if (nrInList > clipList.Count)
        {
            nrInList = 1;
        }
        aS.loop = true;
        aS.clip = clipList[nrInList - 1];
        aS.Play();
    }


}
