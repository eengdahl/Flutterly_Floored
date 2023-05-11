using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuHandler : MonoBehaviour
{
    public Animator birdAnimator;
    public Animator paperAnimator;
    string startGamepaper = "Plane_004|Plane_004Action_001";

    bool mainPanelIsShowing;
    string startGame = "crash in to window";
    public AudioClip pang;
    AudioSource aS;


    SwitchControls switchControls;


    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject exitPanel;




    void Start()
    {
        aS = GetComponent<AudioSource>();
        switchControls = FindAnyObjectByType<SwitchControls>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


    public void StartGameButton()
    {
        paperAnimator.CrossFade(startGamepaper, 0, 0);
        birdAnimator.CrossFade(startGame, 0,0);

        Invoke(nameof(StartGame), 2);
       // Invoke(nameof(playPang), 1.3f);

    }
    public void SettingsMenu()
    {
        settingsPanel.SetActive(false);
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);

    }
    public void ControlsMenu()
    {
        controlsPanel.SetActive(true);
        settingsPanel.SetActive(false);
        mainPanel.SetActive(false);

    }
    public void CreditsMenu()
    {
        creditsPanel.SetActive(true);
        settingsPanel.SetActive(false);

        mainPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        if (settingsPanel.activeSelf == true)
            settingsPanel.SetActive(false);
        if (controlsPanel.activeSelf == true)
            controlsPanel.SetActive(false);
        if (creditsPanel.activeSelf == true)
            creditsPanel.SetActive(false);

            mainPanelIsShowing = !mainPanelIsShowing;
            mainPanel.SetActive(true);
        
    }
    public void ExitGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }









    void playPang()
    {
        aS.PlayOneShot(pang);
    }
    private void StartGame()
    {

         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
