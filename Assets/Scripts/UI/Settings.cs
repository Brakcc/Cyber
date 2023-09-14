using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer;

    //param sons
    [SerializeField] private Slider musicslider;
    [SerializeField] private Slider soundslider;

    public void Start()
    {
        //param sons
        audiomixer.GetFloat("Music", out float musicvalueforslider);
        musicslider.value = musicvalueforslider;

        audiomixer.GetFloat("SoundEff", out float soundvalueforslider);
        soundslider.value = soundvalueforslider;

        //récup des datas depuis le fichier d'enregistrement
        audiomixer.SetFloat("Music", SavePlayerData.LoadMusicVolume());
        musicslider.value = SavePlayerData.LoadMusicVolume();

        audiomixer.SetFloat("SoundEff", SavePlayerData.LoadSFXVolume());
        soundslider.value = SavePlayerData.LoadSFXVolume();
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Music", volume);
        GameManager.gm.musicVol = volume;
    }

    public void SetSoundEffVolume(float volume)
    {
        audiomixer.SetFloat("SoundEff", volume);
        GameManager.gm.sfxVol = volume;
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}