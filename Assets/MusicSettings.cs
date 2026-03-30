using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] GameObject icon;
    [HideInInspector]public const string MUSIC_KEY = "MusicVolume";
    private bool status = true;
    private float volStatus = 0;

    public void SetMusic()
    {
        if (status)
        {
            mixer.SetFloat(MUSIC_KEY, -80);
            volStatus = -80;
            status = false;
            icon.SetActive(true);
        }
        else
        {
            mixer.SetFloat(MUSIC_KEY, 0);
            volStatus = -20;
            status = true;
            icon.SetActive(false);
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY,volStatus);
    }
}
