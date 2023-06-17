using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedCounter : MonoBehaviour
{
    public int seedCount = 0;
    public TextMeshProUGUI seedCountText;
    public GameObject seedCounterPanel;
    public GameObject seed2D;
    public GameObject instatiatePos;

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
        //GameObject seed = Instantiate(seed2D, Vector3.zero, Quaternion.identity, instatiatePos.transform);

        seedCounterPanel.SetActive(true);
        seedCountText.text = seedCount.ToString();
        Invoke(nameof(DisablePanel), 3);
    }

    public void DisablePanel()
    {
        seedCounterPanel.SetActive(false);
    }
}
