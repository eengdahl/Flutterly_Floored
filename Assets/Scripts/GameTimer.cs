using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float startTime;
    private bool timerRunning;
    float elapsedTime;
    string formattedTime;
    void Start()
    {
        // Start the timer when the scene is loaded
        StartTimer();
    }

    void Update()
    {
        // Check if the timer is running and update the elapsed time
        if (timerRunning)
        {
             elapsedTime = Time.time - startTime;
             formattedTime = FormatTime(elapsedTime);
            
        }

    }

    void StartTimer()
    {
        // Set the start time and start the timer
        startTime = Time.time;
        timerRunning = true;
    }

    void StopTimer()
    {
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
    void ExampleStopTimerUsage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopTimer();
            Debug.Log("Elapsed Time: " + formattedTime); // Print elapsed time in seconds and minutes to the console
        }
    }
}