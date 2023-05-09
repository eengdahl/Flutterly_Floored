using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    public bool sugarFilled, milkFilled, flourFilled;
    public bool isFull;
    public string currentIngredient;

    public void ResetSpoon()
    {
        sugarFilled = false;
        milkFilled = false;
        flourFilled = false;
    }

}
