using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapScript : MonoBehaviour
{
    DeathScriptAndCheckPoint deathScript;
    [SerializeField] GameObject[] CheckPointCubes;
    public bool mapUp;
    bool pauseMenuIsShowing = false;
    ActivateButtonsMap mapButtons;
    MenuHandler menuHandler;


    void Start()
    {
        menuHandler = FindObjectOfType<MenuHandler>();
        mapButtons = FindAnyObjectByType<ActivateButtonsMap>();
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();
        gameObject.SetActive(false);
    }

    public void ToggleMap()
    {
        if (!menuHandler.pauseMenuIsShowing)
        {
            SwitchCursor();
            gameObject.SetActive(!gameObject.activeSelf);
            mapButtons.ActiveCheckpoints();
        }
    }

    public void RespawnOne()
    {
        deathScript.respawnTransform = CheckPointCubes[0].transform;
        deathScript.Die();
        ToggleMap();
    }
    public void RespawnTwo()
    {
        deathScript.respawnTransform = CheckPointCubes[1].transform;
        deathScript.Die();
        ToggleMap();
    }
    public void RespawnThree()
    {
        deathScript.respawnTransform = CheckPointCubes[2].transform;
        deathScript.Die();
        ToggleMap();
    }
    public void RespawnFour()
    {
        deathScript.respawnTransform = CheckPointCubes[3].transform;
        deathScript.Die();
        ToggleMap();
    }
    public void RespawnFive()
    {
        deathScript.respawnTransform = CheckPointCubes[4].transform;
        deathScript.Die();
        ToggleMap();
    }
    public void RespawnSix()
    {
        deathScript.respawnTransform = CheckPointCubes[5].transform;
        deathScript.Die();
        ToggleMap();
    }


    public void SwitchCursor()
    {
        if (!menuHandler.pauseMenuIsShowing)
            mapUp = !mapUp;
        if (mapUp) UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        else UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;
    }
}
