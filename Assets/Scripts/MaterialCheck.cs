using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class MaterialCheck : MonoBehaviour
{
    //List of materials in the world, use NameConvention "Material_Counter"
    List<string> Materials = new List<string> { "Material_Counter (Instance)", "Material_Floor (Instance)", "Material_Puzzle (Instance)","Material_Wall (Instance)" };

    private MaterialState ActiveState;

    //Enum of states for the diffrent materials, add in statemachine and List of materials if modified 
    public enum MaterialState
    {
        Counter = 0,
        Floor = 1,
        Puzzle = 2,
        Book = 3,
        Wall = 4,
        None = 5
    }

    //Checking what material Class is colliding with, sending info to statemachine
    private void OnCollisionEnter(Collision other)
    {
        var t = other.gameObject.GetComponent<BoxCollider>().material.name;
        if (t == null)
            StateMachine(5);

        foreach (var item in Materials)
        {
            if (t == item)
            {
                int sendInt = Materials.FindIndex(x => x.Equals(item));
                StateMachine(sendInt);
                break;
            }
        }
    }

    //Might need to insert a "not tuching material.
    //But this might be handled by grounded in player instead       


    public void StateMachine(int NewState = 5)
    {
        ActiveState = (MaterialState)Enum.ToObject(typeof(MaterialState), NewState);

        switch (ActiveState)
        {
            case MaterialState.Counter:
                Debug.Log("Counter");
                break;
            case MaterialState.Floor:
                Debug.Log("Floor");
                break;
            case MaterialState.Puzzle:
                Debug.Log("Puzzle");
                break;
            case MaterialState.Book:
                Debug.Log("Book");
                break;
            case MaterialState.Wall:
                Debug.Log("Cat");
                break;
            case MaterialState.None:
                Debug.Log("None");
                break;
            default:
                break;
        }
    }

    //Collect wich material class is colliding with at this moment. 
    public MaterialState CheckMaterialState()
    {
        return ActiveState;
    }
}
