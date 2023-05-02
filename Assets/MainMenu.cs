using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button quit;
    public Button start;
    public Button settings;

   // public Image settingsMenu;
    public GameObject backFromSettings;
    public GameObject volumeObj;
    public GameObject SFXObj;
    public Slider musicSlider;
    public Slider SFXSlider;
    public AudioMixer mixer;



    public void OnPlayClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitClick()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnSettingsClick()
    {
        start.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        backFromSettings.SetActive(true);
        volumeObj.SetActive(true);
        SFXObj.SetActive(true);
    }
    public void OnSettingsBackClick()
    {
        start.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        settings.gameObject.SetActive(true);
        backFromSettings.SetActive(false);
        volumeObj.SetActive(false);
        SFXObj.SetActive(false);
    }
}
