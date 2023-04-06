using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VitrinScript : MonoBehaviour
{
    public enum CatPattern
    {
        patrol = 0,
        stop = 1,
        attack = 2,
    }
    public List<Transform> walkPath;
    public Transform currentTarget;
    [SerializeField] private Light eyes;


    private void Awake()
    {

        eyes = GetComponent<Light>();
        walkPath = new List<Transform>();
        var temp = GameObject.FindGameObjectsWithTag("WalkPath");

        foreach (var item in temp)
        {
            walkPath.Add(item.transform);
        }
        StateMachine();
    }

    public IEnumerator StateMachine(CatPattern newState = CatPattern.patrol)
    {
        CatPattern currentState = newState;

            
       yield return currentState;
    }
    //void Update()
    //{
    //    //Run only if not running
    //    if (!speaking)
    //        StartCoroutine(IsSpeaking());
    //}

    //Then set the boolean variable to false before breaking out of the switch statement in the coroutine.

    //IEnumerator IsSpeaking()
    //{
    //    speaking = true;

    //    switch (dialogueNumber)
    //    {
    //        case 0:

    //            yield return null;

    //            speaking = false;
    //            break;

    //        case 1:

    //            functionsScript.IsSpeaking();
    //            yield return new WaitForSeconds(3);
    //            functionsScript.Silence();

    //            speaking = false;
    //            yield break;

    //        case 2:

    //            functionsScript.IsSpeaking();
    //            yield return new WaitForSeconds(6);
    //            functionsScript.Silence();

    //            speaking = false;
    //            yield break;
    //    }
    //    yield break;
    //}




}
