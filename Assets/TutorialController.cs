using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    GameObject moveTutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator ToggleTutorial(string tutorial)
    {
        if (tutorial == "Move")
        {
            moveTutorial.SetActive(false);
        }
        yield return null;

    }
}
