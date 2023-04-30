using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] Button retryButton;
    [SerializeField] Button quitButton;
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        gameObject.SetActive(false);
        retryButton.onClick.AddListener(delegate { RestartLevel(); });
        quitButton.onClick.AddListener(delegate { QuitToMenu(); });
    }

    private void RestartLevel()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void QuitToMenu()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void DeathHandler()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

}
