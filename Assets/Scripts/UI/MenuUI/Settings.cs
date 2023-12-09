using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private AudioMixer audiomixer;

        //param sons
        [SerializeField] private Slider musicslider;
        [SerializeField] private Slider soundslider;

        public void SetVolume(float volume)
        {
            audiomixer.SetFloat("Music", volume);
        }

        public void SetSoundEffVolume(float volume)
        {
            audiomixer.SetFloat("SoundEff", volume);
        }

        public void SetFullScreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}