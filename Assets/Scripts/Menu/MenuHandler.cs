using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;
    //[SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject exitGamePanel;
    //[SerializeField] private GameObject musicSource;
    [SerializeField] public bool pauseMenuIsShowing;
    bool checkmenu;
    MapScript mapScript;

    private void Start()
    {
        mapScript = FindObjectOfType<MapScript>();
    }

    public void ButtonInput(InputAction.CallbackContext input)

    {
        if (input.started)
        {
            if (settingsPanel.activeSelf == false && controlsPanel.activeSelf == false && creditsPanel.activeSelf == false && exitGamePanel.activeSelf == false)
            {
                if (pauseMenuIsShowing == true)
                {
                    pauseMenuIsShowing = !pauseMenuIsShowing;
                    pausePanel.SetActive(false);
                    Time.timeScale = 1;

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else if (pauseMenuIsShowing == false)
                {
                    if (mapScript.mapUp)
                    {
                        mapScript.ToggleMap();
                    }
                    pauseMenuIsShowing = !pauseMenuIsShowing;
                    pausePanel.SetActive(true);
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }

                checkmenu = true;
            }
            else if (controlsPanel.activeSelf == true || settingsPanel.activeSelf == true || creditsPanel.activeSelf == true || exitGamePanel.activeSelf == true)
            {
                if (settingsPanel.activeSelf == true)
                    settingsPanel.SetActive(false);
                if (controlsPanel.activeSelf == true)
                    controlsPanel.SetActive(false);
                if (creditsPanel.activeSelf == true)
                    creditsPanel.SetActive(false);
                if (exitGamePanel.activeSelf == true)
                    exitGamePanel.SetActive(false);
                if (pauseMenuIsShowing == false)
                {
                    pauseMenuIsShowing = !pauseMenuIsShowing;
                    pausePanel.SetActive(true);
                }

            }
        }

        if (pauseMenuIsShowing == false && settingsPanel.activeSelf == false && controlsPanel.activeSelf == false && creditsPanel.activeSelf == false && exitGamePanel.activeSelf == false && checkmenu)
        {
            pausePanel.SetActive(false);
            checkmenu = false;
        }
        //pausePanel.SetActive(pauseMenuIsShowing);
    }

    public void ExitGame()
    {
        exitGamePanel.SetActive(true);
        pausePanel.SetActive(false);
        pauseMenuIsShowing = !pauseMenuIsShowing;
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void StartGame()
    {
        //startgame function
        //mainMenu.SetActive(false);
        //musicSource.SetActive(true);

    }
    public void SettingsMenu()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
        pauseMenuIsShowing = !pauseMenuIsShowing;
    }
    public void ControlsMenu()
    {
        controlsPanel.SetActive(true);
        pausePanel.SetActive(false);

        pauseMenuIsShowing = !pauseMenuIsShowing;
    }
    public void CreditsMenu()
    {
        creditsPanel.SetActive(true);
        pausePanel.SetActive(false);

        pauseMenuIsShowing = !pauseMenuIsShowing;

    }
    public void ResumeGame()
    {
        pauseMenuIsShowing = !pauseMenuIsShowing;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void BackToMenu()
    {
        if (settingsPanel.activeSelf == true)
            settingsPanel.SetActive(false);
        if (controlsPanel.activeSelf == true)
            controlsPanel.SetActive(false);
        if (creditsPanel.activeSelf == true)
            creditsPanel.SetActive(false);
        if (exitGamePanel.activeSelf == true)
            exitGamePanel.SetActive(false);
        if (pauseMenuIsShowing == false)
        {
            pauseMenuIsShowing = !pauseMenuIsShowing;
            pausePanel.SetActive(true);
        }
    }
}
