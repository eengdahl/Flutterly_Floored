using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedCounter : MonoBehaviour
{
    private int seedCount = 0;
    public TextMeshProUGUI seedCountText;

    public void AddSeed(int value)
    {
        seedCount += value;
    }
    public int GetSeedCount()
    {
        return seedCount;
    }
    public void SetSeedCount(int seedCount)
    {
        seedCountText.text = "Seeds: " + seedCount.ToString();
    }
}
