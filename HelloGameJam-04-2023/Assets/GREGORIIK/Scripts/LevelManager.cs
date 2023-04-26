using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene currentScene;
    PlayerHealth playerHealth;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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

    private void ChangeBackgroundMusic()
    {

    }

}
