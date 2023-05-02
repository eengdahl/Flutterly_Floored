using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapScript : MonoBehaviour
{
    DeathScriptAndCheckPoint deathScript;
    [SerializeField] GameObject[] CheckPointCubes;
    bool mapUp;
    bool pauseMenuIsShowing = false;

    void Start()
    {
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();
        gameObject.SetActive(false);
    }

    public void ToggleMap()
    {
        Cursor.visible = !Cursor.visible;      
        gameObject.SetActive(!gameObject.activeSelf);
        mapUp = !mapUp;
        if (mapUp)      UnityEngine.Cursor.lockState = CursorLockMode.None;
        else UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void RespawnOne()
    {
        deathScript.respawnTransform = CheckPointCubes[0].transform;
        deathScript.Die();
    }
    public void RespawnTwo()
    {
        deathScript.respawnTransform = CheckPointCubes[1].transform;
        deathScript.Die();
    }
    public void RespawnThree()
    {
        deathScript.respawnTransform = CheckPointCubes[2].transform;
        deathScript.Die();
    }
    public void RespawnFour()
    {
        deathScript.respawnTransform = CheckPointCubes[3].transform;
        deathScript.Die();
    }
    public void RespawnFive()
    {
        deathScript.respawnTransform = CheckPointCubes[4].transform;
        deathScript.Die();
    }
    public void RespawnSix()
    {
        deathScript.respawnTransform = CheckPointCubes[5].transform;
        deathScript.Die();
    }

}
