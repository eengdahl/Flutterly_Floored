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

    void Start()
    {
        AudioHandler.Instance.ChangeEffectsVolume(_slider.value);
        AudioHandler.Instance.ChangeMusicVolume(_slider.value);

        if (_effectsVolume)
            _slider.onValueChanged.AddListener(value => AudioHandler.Instance.ChangeEffectsVolume(value));

        if (_musicVolume)
            _slider.onValueChanged.AddListener(value => AudioHandler.Instance.ChangeMusicVolume(value));
    }
    public void SetMasterVolume(float _sliderValue)
    {
        _audioMixer.SetFloat("Master", Mathf.Log10(_sliderValue) * 20);
    }
    public void SetEffectsVolume(float _sliderValue)
    {
        _audioMixer.SetFloat("SFX", Mathf.Log10(_sliderValue) * 20);
    }
    public void SetMusicVolume(float _sliderValue)
    {
        _audioMixer.SetFloat("BackGround", Mathf.Log10(_sliderValue) * 20);
    }
}
