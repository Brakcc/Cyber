using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audiosource;
    private int index = 0;

    public int musicID;
    [HideInInspector] public bool needToSwitch;

    public AudioMixerGroup soundeffmix;

    public static AudioManager ad;
    private void Awake()
    {
        ad = this;
    }

    private void Start()
    {
        needToSwitch = false;
        //musicID = SavePlayerData.LoadIDMusic(); ;
        audiosource.clip = playlist[musicID];
        audiosource.Play();
    }

    private void Update()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.clip = playlist[musicID];
            audiosource.Play();
        }
    }

    public void PlayNextSong()
    {
        index = (index + 1) % playlist.Length;
        audiosource.clip = playlist[index];
        audiosource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject TempGO = new("TempAudio");
        TempGO.transform.position = pos;
        AudioSource audioSource = TempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundeffmix;
        audioSource.Play();
        Destroy(TempGO, clip.length);
        return audioSource;
    }
}