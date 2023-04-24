using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource sfxAudioSource;
    AudioSource[] audioSources;
    MainMenuUIController mainMenuUIController;
    void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        mainMenuUIController = FindObjectOfType<MainMenuUIController>().GetComponent<MainMenuUIController>();
        SetAudioVolume();
    }

    public void SetAudioVolume()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.tag == "Music") 
            {
                source.volume = mainMenuUIController.mainVolumeSlider.value * mainMenuUIController.musicVolumeSlider.value;
            }
            else if (source.tag == "Dialog")
            {
                source.volume = mainMenuUIController.mainVolumeSlider.value * mainMenuUIController.dialogsVolumeSlider.value;
            }
            else if (source.tag == "SFX")
            {
                source.volume = mainMenuUIController.mainVolumeSlider.value * mainMenuUIController.sfxVolumeSlider.value;
            }
            else
            {
                Debug.Log("tag error");
            }
        }
    }

    public void PlayRandomDoorSFX()
    {
        if (sfxAudioSource.isPlaying)
        {
            sfxAudioSource.Stop();
        }
        AudioClip randomDoorClip = audioClips[Random.Range(0, audioClips.Length)];
        sfxAudioSource.PlayOneShot(randomDoorClip);
    }
}
