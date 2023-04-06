using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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

    private void StateMachine(CatPattern newState = CatPattern.patrol)
    {
        CatPattern currentState = newState;
        switch (currentState)
        {
            case CatPattern.patrol:
                currentTarget = walkPath[Random.Range(0, walkPath.Count - 1)];
                transform.position = currentTarget.position;
                Debug.Log("patrol");
                StateMachine(CatPattern.stop);

                break;
            case CatPattern.stop:
                Debug.Log("stop");
                System.Threading.Thread.Sleep(3000);

                StateMachine(CatPattern.attack);

                break;
            case CatPattern.attack:
                Debug.Log("Attack");
                System.Threading.Thread.Sleep(3000);
                StateMachine();
                break;
            default:
                break;
        }
    }



}
