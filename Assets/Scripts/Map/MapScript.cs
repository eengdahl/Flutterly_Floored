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
    public bool inVitrin;
    SwitchControls switchControls;
    [SerializeField]AudioSource aS;

    void Start()
    {
       
        switchControls = FindAnyObjectByType<SwitchControls>();
        menuHandler = FindObjectOfType<MenuHandler>();
        mapButtons = FindAnyObjectByType<ActivateButtonsMap>();
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();
        gameObject.SetActive(false);
    }

    public void ToggleMap()
    {
        if (!menuHandler.pauseMenuIsShowing && !inVitrin)
        {
            if (mapUp)
            {
                Time.timeScale = 1;
            }
            else
            {
                
                aS.Play();
                Time.timeScale = 0;
            }
            SwitchCursor();
            gameObject.SetActive(!gameObject.activeSelf);
            mapButtons.ActiveCheckpoints();
        }
    }
    private void Update()
    {
        buttonPressed(KeyCode.Alpha1, mapButtons.onOrOff[0],0);
        buttonPressed(KeyCode.Alpha2, mapButtons.onOrOff[1],1);
        buttonPressed(KeyCode.Alpha3, mapButtons.onOrOff[2],2);
        buttonPressed(KeyCode.Alpha4, mapButtons.onOrOff[3],3);
        buttonPressed(KeyCode.Alpha5, mapButtons.onOrOff[4],4);
        buttonPressed(KeyCode.Alpha6, mapButtons.onOrOff[5],5);


    }
    public void RespawnOne()
    {
        deathScript.respawnTransform = CheckPointCubes[0].transform;
        deathScript.Teleport();
        ToggleMap();
    }
    public void RespawnTwo()
    {
        deathScript.respawnTransform = CheckPointCubes[1].transform;
        deathScript.Teleport();
        ToggleMap();
    }
    public void RespawnThree()
    {
        deathScript.respawnTransform = CheckPointCubes[2].transform;
        deathScript.Teleport();
        ToggleMap();
    }
    public void RespawnFour()
    {
        deathScript.respawnTransform = CheckPointCubes[3].transform;
        deathScript.Teleport();
        ToggleMap();
    }
    public void RespawnFive()
    {
        deathScript.respawnTransform = CheckPointCubes[4].transform;
        deathScript.Teleport();
        ToggleMap();
    }
    public void RespawnSix()
    {
        deathScript.respawnTransform = CheckPointCubes[5].transform;
        deathScript.Teleport();
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

    void buttonPressed(KeyCode key, bool unlocked, int index)
    {
        if (Input.GetKeyDown(key)&& unlocked)
        {
            deathScript.respawnTransform = CheckPointCubes[index].transform;
            deathScript.Teleport();
            ToggleMap();
        }
    }
}
