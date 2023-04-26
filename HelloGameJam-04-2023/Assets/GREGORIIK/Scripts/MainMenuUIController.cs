using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [Header("G³ówne przyciski")]
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
    [Header("Kontynuuj?")]
    [SerializeField] GameObject newGameWindow;
    [SerializeField] Button continueButton;
    [SerializeField] Button newGameButton;
    [Header("Credits")]
    [SerializeField] Button creditsButton;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] TextMeshProUGUI creditsText;
    [SerializeField] Button exitCreditsButton;
    [SerializeField] float creditsTextStartPos;
    [SerializeField] float scrollSpeed = 10;

    private bool isOptionsPanelVisible = false;

    float startMainVolume;
    float startMusicVolume;
    float startDialogsVolume;
    float startSFXVolume;
    int highestLevelAchieved;

    string mainVolumeTag = "mainVolume";
    string musicVolumeTag = "musicVolume";
    string dialogVolumeTag = "dialogVolume";
    string sfxVolumeTag = "sfxVolume";
    string highestLevelTag = "highestLevel";

    AudioPlayer audioPlayer;
    LevelManager levelManager;
    Scene currentScene;

    void Awake()
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat(mainVolumeTag, 0.5f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolumeTag, 0.5f);
        dialogsVolumeSlider.value = PlayerPrefs.GetFloat(dialogVolumeTag, 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(sfxVolumeTag, 0.5f);


        audioPlayer = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
        levelManager = FindAnyObjectByType<LevelManager>();
        currentScene = SceneManager.GetActiveScene();

        optionsButton.onClick.AddListener(delegate { SwitchOptionsPanelVisible(); });
        startButton.onClick.AddListener(delegate { StartGame(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });

        cancelButton.onClick.AddListener(delegate { CancelChanges(); audioPlayer.PlayRandomDoorSFX(); });
        confirmButton.onClick.AddListener(delegate { ConfirmChanges(); audioPlayer.PlayRandomDoorSFX(); });

        mainVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); });
        dialogsVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); audioPlayer.PlayRandomDoorSFX(); });

        newGameButton.onClick.AddListener(delegate { SceneManager.LoadScene(1); });
        continueButton.onClick.AddListener(delegate { SceneManager.LoadScene(highestLevelAchieved); });

        creditsButton.onClick.AddListener(delegate { creditsScreen.SetActive(true);
                                                     StartCoroutine(ScrollCredits());
                                                     SwitchOptionsPanelVisible(); });
        exitCreditsButton.onClick.AddListener(delegate { ExitCredits(); });

        SetVolumeText();
        optionsPanel.SetActive(false);
        newGameWindow.SetActive(false);
        creditsScreen.SetActive(false);
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
        if (!PlayerPrefs.HasKey(highestLevelTag) || highestLevelAchieved == 1) SceneManager.LoadScene(currentScene.buildIndex + 1);
        else newGameWindow.SetActive(true);
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

    private void ExitCredits()
    {
        StopAllCoroutines();
        creditsScreen.SetActive(false);
        creditsText.rectTransform.position = new Vector3(creditsText.rectTransform.position.x, creditsTextStartPos, 0);
    }

    IEnumerator ScrollCredits()
    {
        while (true) 
        {
            float creditsYPos = creditsText.rectTransform.position.y;
            creditsYPos += Time.deltaTime * scrollSpeed;
            creditsText.rectTransform.position = new Vector3(creditsText.rectTransform.position.x, creditsYPos, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    private void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

}
