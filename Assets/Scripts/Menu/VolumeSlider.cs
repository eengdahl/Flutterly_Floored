using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _musicVolume, _effectsVolume;
    [SerializeField] AudioMixer _audioMixer;
    private AudioSource aS;

    void Start()
    {
        AudioHandler.Instance.ChangeEffectsVolume(_slider.value);
        AudioHandler.Instance.ChangeMusicVolume(_slider.value);

        if (_effectsVolume)
        {
            aS = GetComponent<AudioSource>();
            if (!PlayerPrefs.HasKey("effectsVolume"))
            {
                PlayerPrefs.SetFloat("effectsVolume", 1);
                Load();
            }
            else
            {
                Load();
            }
            _slider.onValueChanged.AddListener(value => AudioHandler.Instance.ChangeEffectsVolume(value));
        }

        if (_musicVolume)
        {
            aS = null;
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
                Load();
            }
            else
            {
                Load();
            }

            _slider.onValueChanged.AddListener(value => AudioHandler.Instance.ChangeMusicVolume(value));
        }


    }
    public void SetMasterVolume(float _sliderValue)
    {
        _audioMixer.SetFloat("Master", Mathf.Log10(_sliderValue) * 20);
    }
    public void SetEffectsVolume(float _sliderValue)
    {
        _audioMixer.SetFloat("SFX", Mathf.Log10(_sliderValue) * 20);
        Save();
        aS.Play();
    }
    public void SetMusicVolume(float _sliderValue)
    {
        _audioMixer.SetFloat("Background", Mathf.Log10(_sliderValue) * 20);
        Save();
    }

    private void Load()
    {
        if (_effectsVolume)
            _slider.value = PlayerPrefs.GetFloat("effectsVolume");

        if (_musicVolume)
            _slider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        if (_effectsVolume)
            PlayerPrefs.SetFloat("effectsVolume", _slider.value);

        if (_musicVolume)
            PlayerPrefs.SetFloat("musicVolume", _slider.value);
    }
}

