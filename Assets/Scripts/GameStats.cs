using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStats : MonoBehaviour
{
    public int seedsCollected;
    public float time;
    public int maxAmountOfSeeds;
    [SerializeField] GameObject endingCanvas;
    [SerializeField] SwitchControls switchControls;
    [SerializeField] SeedCounter seedCounter;
    
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI seedsCollectedText;
    //Singleton variables
    private static GameStats _instance;
    public static GameStats Instance { get { return _instance; } }
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetStats()
    {
        seedsCollected = seedCounter.seedCount;
        GameTimer.Instance.StopTimerUsage();
        
    }
    public void ShowStats()
    {
        SetStats();
        timeText.text = "Time: " + FormatTime(time);
        seedsCollectedText.text = "Seeds: " + seedsCollected;
        endingCanvas.SetActive(true);
        switchControls.SwitchToNoInput();
        switchCursor();
        

        
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int hundreds = Mathf.FloorToInt((time * 100f) % 100f); // Calculate the two hundreds numbers
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundreds);
    }
    void switchCursor()
    {
       
        
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !Cursor.visible;

    }
}
