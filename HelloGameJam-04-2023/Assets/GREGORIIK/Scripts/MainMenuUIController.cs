using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;

    [SerializeField] Button startButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;
    [Header("Opcje")]
    [SerializeField] TextMeshProUGUI mainVolumeText;
    [SerializeField] public Slider mainVolumeSlider;
    [SerializeField] TextMeshProUGUI dialogsVolumeText;
    [SerializeField] public Slider dialogsVolumeSlider;
    [SerializeField] TextMeshProUGUI musicVolumeText;
    [SerializeField] public Slider musicVolumeSlider;
    [SerializeField] TextMeshProUGUI sfxVolumeText;
    [SerializeField] public Slider sfxVolumeSlider;
    [SerializeField] Button cancelButton;
    [SerializeField] Button confirmButton;

    [SerializeField] Button creditsButton;


    private bool isOptionsPanelVisible = false;

    float startMainVolume;
    float startMusicVolume;
    float startDialogsVolume;
    float startSFXVolume;

    string mainVolumeTag = "mainVolume";
    string musicVolumeTag = "musicVolume";
    string dialogVolumeTag = "dialogVolume";
    string sfxVolumeTag = "sfxVolume";

    AudioPlayer audioPlayer;

    void Awake()
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat(mainVolumeTag, 0.5f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolumeTag, 0.5f);
        dialogsVolumeSlider.value = PlayerPrefs.GetFloat(dialogVolumeTag, 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(sfxVolumeTag, 0.5f);

        audioPlayer = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
        optionsButton.onClick.AddListener(delegate { SwitchOptionsPanelVisible(); });
        startButton.onClick.AddListener(delegate { StartGame(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });

        cancelButton.onClick.AddListener(delegate { CancelChanges(); audioPlayer.PlayRandomDoorSFX(); });
        confirmButton.onClick.AddListener(delegate { ConfirmChanges(); audioPlayer.PlayRandomDoorSFX(); });
        mainVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); });
        dialogsVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); audioPlayer.PlayRandomDoorSFX(); });

        SetVolumeText();
        optionsPanel.SetActive(false);
    }

    private void GetAudioVolume()
    {
        startMainVolume = mainVolumeSlider.value;
        startMusicVolume = musicVolumeSlider.value;
        startDialogsVolume = dialogsVolumeSlider.value;
        startSFXVolume = sfxVolumeSlider.value;
    }

    private void ChangeAudioVolume()
    {
        SetVolumeText();
        audioPlayer.SetAudioVolume();
    }

    private void SetVolumeText()
    {
        mainVolumeText.text = "G³oœnoœæ: " + (int)(mainVolumeSlider.value * 100);
        dialogsVolumeText.text = "Dialogi: " + (int)(dialogsVolumeSlider.value * 100);
        musicVolumeText.text = "Muzyka: " + (int)(musicVolumeSlider.value * 100);
        sfxVolumeText.text = "SFX: " + (int)(sfxVolumeSlider.value * 100);
    }

    private void StartGame()
    {
        Debug.Log("ten przycisk uruchomi gre");
        audioPlayer.PlayRandomDoorSFX();
    }

    private void SwitchOptionsPanelVisible()
    {
        isOptionsPanelVisible = !isOptionsPanelVisible;
        optionsPanel.SetActive(isOptionsPanelVisible);
        ToggleMainButtonsState(!isOptionsPanelVisible);
        GetAudioVolume();
        audioPlayer.PlayRandomDoorSFX();
    }

    private void ToggleMainButtonsState(bool state)
    {
        startButton.interactable = state;
        optionsButton.interactable = state;
        quitButton.interactable = state;
    }

    private void ConfirmChanges()
    {
        SwitchOptionsPanelVisible();
        PlayerPrefs.SetFloat(mainVolumeTag, mainVolumeSlider.value);
        PlayerPrefs.SetFloat(musicVolumeTag, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(dialogVolumeTag, dialogsVolumeSlider.value);
        PlayerPrefs.SetFloat(sfxVolumeTag, sfxVolumeSlider.value);
    }

    private void CancelChanges()
    {
        mainVolumeSlider.value = startMainVolume;
        musicVolumeSlider.value = startMusicVolume;
        dialogsVolumeSlider.value = startDialogsVolume;
        sfxVolumeSlider.value = startSFXVolume;
        SwitchOptionsPanelVisible();
    }

    private void QuitGame()
    {
        Debug.Log("Exiting game");
        PlayerPrefs.Save();
        Application.Quit();
    }

}
