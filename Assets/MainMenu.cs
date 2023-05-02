using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image settingsMenu;
    public GameObject backFromSettings;
    public GameObject volumeObj;
    public GameObject SFXObj;
    public Slider musicSlider;
    public Slider SFXSlider;
    public AudioMixer mixer;



    public void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            if (settingsMenu.enabled)
            {
                settingsMenu.enabled = false;
            }
        }
    }


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
        settingsMenu.enabled = true;
        backFromSettings.SetActive(true);
        volumeObj.SetActive(true);
        SFXObj.SetActive(true);
    }
    public void OnSettingsBackClick()
    {
        settingsMenu.enabled = false;
        backFromSettings.SetActive(false);
        volumeObj.SetActive(false);
        SFXObj.SetActive(false);
    }
}
