using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScreen : MonoBehaviour
{
    [Header("Credits")]
    [SerializeField] GameObject creditsScreen;
    [SerializeField] TextMeshProUGUI creditsText;
    [SerializeField] Button exitCreditsButton;
    [SerializeField] float scrollSpeed = 10;
    LevelManager levelManager;

    bool startScrolling = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        levelManager = FindObjectOfType<LevelManager>();
        exitCreditsButton.onClick.AddListener(delegate { ExitCredits(); });

        creditsScreen.SetActive(false);
        Invoke(nameof(ScrollCredits), 8f);
    }

    private void Update()
    {
        if (startScrolling) { ScrollCredits(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            levelManager.OnLevelFinished();
        }
    }

    private void ExitCredits()
    {
        SceneManager.LoadScene(0);
    }

    private void ScrollCredits()
    {
        if (startScrolling == false)
        {
            startScrolling = true;
            creditsScreen.SetActive(true);

        }
        float creditsYPos = creditsText.rectTransform.position.y;
        creditsYPos += Time.deltaTime * scrollSpeed;
        creditsText.rectTransform.position = new Vector3(creditsText.rectTransform.position.x, creditsYPos, 0);
    }

}
