using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource sfxAudioSource;
    List<AudioSource> audioSources;

    MainMenuUIController mainMenuUIController;
    void Start()
    {
        audioSources = new List<AudioSource>();
        audioSources.AddRange(FindObjectsOfType<AudioSource>());
        mainMenuUIController = FindObjectOfType<MainMenuUIController>().GetComponent<MainMenuUIController>();
        SetAudioVolume();
    }

    public void SetAudioVolume()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source == null)
            {
                audioSources.Remove(source);
                return;
            }
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
                source.volume = mainMenuUIController.mainVolumeSlider.value * mainMenuUIController.dialogsVolumeSlider.value;
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
