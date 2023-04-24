using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    AudioSource[] audioSources;
    MainMenuUIController mainMenuUIController;

    float mainVolume;
    void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        mainMenuUIController = gameObject.GetComponent<MainMenuUIController>();
    }

}
