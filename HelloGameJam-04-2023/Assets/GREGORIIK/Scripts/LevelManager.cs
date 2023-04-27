using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene currentScene;
    PlayerHealth playerHealth;
    BackgroundMusic backgroundMusic;
    AudioLoader audioLoader;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioLoader = FindObjectOfType<AudioLoader>();
        backgroundMusic = FindObjectOfType<BackgroundMusic>().GetComponent<BackgroundMusic>();
        if (playerHealth != null) playerHealth = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
        currentScene = SceneManager.GetActiveScene();
    }

    public void OnPlayerDeath()
    {
        playerHealth.ResetPlayerHealth();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void OnLevelFinished()
    {
        playerHealth.ResetPlayerHealth();
        PlayerPrefs.SetInt("highestLevel", currentScene.buildIndex + 1); // test this!
        SceneManager.LoadScene(currentScene.buildIndex+1);
    }
}
