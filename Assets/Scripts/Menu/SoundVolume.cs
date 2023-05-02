using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider SFXSlider;
    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume); 
       //PlayerPrefs.GetFloat("SFXVolume")
    }
   

    public void SetMusicVolume(float sliderValue)
    {
        mixer.SetFloat("BackGroundVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
}
