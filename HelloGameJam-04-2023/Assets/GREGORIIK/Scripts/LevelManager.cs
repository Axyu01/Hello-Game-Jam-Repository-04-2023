using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    Scene currentScene;
    PlayerHealth playerHealth;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerHealth = FindObjectOfType<PlayerHealth>();
        currentScene = SceneManager.GetActiveScene();
    }

    public void OnPlayerDeath()
    {
        playerHealth.ResetPlayerHealth();
        playerHealth.IsDead = false;
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void OnLevelFinished()
    {
        if (playerHealth != null) playerHealth.ResetPlayerHealth();
        if (currentScene.buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("highestLevel", currentScene.buildIndex); // test this!
            PlayerPrefs.Save();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
        else
        {
            PlayerPrefs.SetInt("highestLevel", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }
        
    }
}
