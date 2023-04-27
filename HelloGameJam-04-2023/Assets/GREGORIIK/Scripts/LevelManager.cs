using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    Scene currentScene;
    PlayerHealth playerHealth;
    //BackgroundMusic backgroundMusic;
    //AudioLoader audioLoader;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //audioLoader = FindObjectOfType<AudioLoader>();
        //backgroundMusic = FindObjectOfType<BackgroundMusic>().GetComponent<BackgroundMusic>();
        if (playerHealth != null) playerHealth = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
        currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N)) { OnLevelFinished(); }
    }

    public void OnPlayerDeath()
    {
        DeathHandler();
        playerHealth.ResetPlayerHealth();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void OnLevelFinished()
    {
        if (playerHealth != null) playerHealth.ResetPlayerHealth();
        PlayerPrefs.SetInt("highestLevel", currentScene.buildIndex + 1); // test this!
        PlayerPrefs.Save();
        SceneManager.LoadScene(currentScene.buildIndex+1);
    }

    private void DeathHandler()
    {
        Time.timeScale = 0;
        //dodaæ wyœwietlanie deathscreenu

    }

}
