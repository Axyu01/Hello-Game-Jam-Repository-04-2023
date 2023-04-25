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
        playerHealth = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
        currentScene = SceneManager.GetActiveScene();
    }

    public void OnPlayerDeath()
    {
        playerHealth.currentHealth = playerHealth.MaxHealth;
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void OnLevelFinished()
    {
        playerHealth.currentHealth = playerHealth.MaxHealth;
        SceneManager.LoadScene(currentScene.buildIndex+1);
    }

}
