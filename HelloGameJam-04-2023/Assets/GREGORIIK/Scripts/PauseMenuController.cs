using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] Canvas pauseMenu;
    [SerializeField] GameObject confirmWindow;
    [SerializeField] Button returnButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseMenu.enabled = false;
        confirmWindow.SetActive(false);
        returnButton.onClick.AddListener( delegate { TogglePauseMenu(pauseMenu.enabled); });
        quitButton.onClick.AddListener(delegate { ToggleConfirmWindow(); });
        confirmButton.onClick.AddListener(delegate { ReturnToMainMenu(); });
        cancelButton.onClick.AddListener(delegate { ToggleConfirmWindow(); });
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && !pauseMenu.enabled)
        {
            TogglePauseMenu(false);
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && pauseMenu.enabled) 
        {
            TogglePauseMenu(true);
        }
    }

    private void TogglePauseMenu(bool value)
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.enabled = !value;
        Cursor.visible = !value;
        TogglePauseState();
    }

    private void TogglePauseState()
    {
        if (pauseMenu.enabled) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    private void ToggleConfirmWindow()
    {
        confirmWindow.SetActive(!confirmWindow.activeSelf);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
