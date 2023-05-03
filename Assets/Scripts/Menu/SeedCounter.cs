using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedCounter : MonoBehaviour
{
    private int seedCount = 0;
    public TextMeshProUGUI seedCountText;
    public Camera cam;

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
        cam.enabled = true;
        seedCountText.text = seedCount.ToString();
        Invoke(nameof(DisableCam), 3);
    }

    public void DisableCam()
    {
        cam.enabled = false;
    }
}
