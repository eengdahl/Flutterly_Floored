using HoudiniEngineUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevShortcuts : MonoBehaviour
{
    List<Transform> respawnPoints;
    DeathScriptAndCheckPoint deathScript;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoints = new List<Transform>();
        deathScript = FindAnyObjectByType<DeathScriptAndCheckPoint>();

        // var temp = GetComponentInChildren<Transform>();
        var temp = GetComponentsInChildren<Transform>();


        foreach (Transform item in temp)
        {
            if (item.ToString().Contains("RespawnCube"))
            {
                respawnPoints.Add(item);
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        SceneManager.LoadScene(0);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad0))
    //    {
    //        deathScript.NewCheckpoint(respawnPoints[0]);
    //        deathScript.Die();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad1))
    //    {
    //        deathScript.NewCheckpoint(respawnPoints[1]);
    //        deathScript.Die();

    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad2))
    //    {
    //        deathScript.NewCheckpoint(respawnPoints[2]);
    //        deathScript.Die();

    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad3))
    //    {
    //        deathScript.NewCheckpoint(respawnPoints[3]);
    //        deathScript.Die();

    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad4))
    //    {
    //        deathScript.NewCheckpoint(respawnPoints[4]);
    //        deathScript.Die();

    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad5))
    //    {
    //        deathScript.NewCheckpoint(respawnPoints[5]);
    //        deathScript.Die();

    //    }



    //}
}
