using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioSource audioSource, musicSource;

    public void ChangeEffectsVolume(float Value)
    {
        audioSource.volume = Value;
    }
    public void ChangeMusicVolume(float Value)
    {
        musicSource.volume = Value;
    }
    public void ToggleSoundEffects()
    {
        audioSource.mute = !audioSource.mute;
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
}
