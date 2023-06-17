using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float startTime;
    private bool timerRunning;
    float elapsedTime;
    string formattedTime;
    GameStats gameStats;

    //Singleton variables
    private static GameTimer _instance;
    public static GameTimer Instance { get { return _instance; } }

    void Start()
    {
        // Start the timer when the scene is loaded
        StartTimer();
        if (_instance == null)
        {
            _instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        // Check if the timer is running and update the elapsed time
        if (timerRunning)
        {
            elapsedTime = Time.time - startTime;


        }
        //StopTimerUsage();

    }

    void StartTimer()
    {
        // Set the start time and start the timer
        startTime = Time.time;
        timerRunning = true;
    }

    void StopTimer()
    {
        formattedTime = FormatTime(elapsedTime);
        // Stop the timer
        timerRunning = false;
    }

    // Format the elapsed time into seconds and minutes
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Example usage of stopping the timer
    public void StopTimerUsage()
    {

        GameStats.Instance.time = elapsedTime;
        StopTimer();
        Debug.Log("Elapsed Time: " + formattedTime); // Print elapsed time in seconds and minutes to the console

    }
}