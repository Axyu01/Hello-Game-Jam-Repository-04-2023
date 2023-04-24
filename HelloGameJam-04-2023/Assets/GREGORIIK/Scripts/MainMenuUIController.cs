using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;

    [SerializeField] Button startButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;

    [SerializeField] Button cancelButton;
    [SerializeField] Button confirmButton;

    [SerializeField] Button creditsButton;

    void Start()
    {
        optionsPanel.SetActive(false);
        optionsButton.onClick.AddListener(delegate { optionsPanel.SetActive(true); });
        startButton.onClick.AddListener(delegate { StartGame(); });
    }

    private void StartGame()
    {
        Debug.Log("ten przycisk uruchomi gre"); 
    }

}
