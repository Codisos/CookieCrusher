using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [HideInInspector] public static AudioManager instance;
    [HideInInspector]
    public const string MUSIC_KEY = "MusicVolume";

    private void Awake()
    {
        LoadMusicVolume();
    }

    void LoadMusicVolume()
    {
        mixer.SetFloat(MusicSettings.MUSIC_KEY, PlayerPrefs.GetFloat(MUSIC_KEY, -80));
    }
}
