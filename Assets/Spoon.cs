using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    public bool sugarFilled, eggFilled, flourFilled;
    public bool isFull;
    public string currentIngredient;

    public void ResetSpoon()
    {
        sugarFilled = false;
        eggFilled = false;
        flourFilled = false;
    }

}
