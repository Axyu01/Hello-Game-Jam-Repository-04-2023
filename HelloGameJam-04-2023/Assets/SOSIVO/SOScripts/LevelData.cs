using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelData", menuName = "Player Data/LevelData")]
public class LevelData : ScriptableObject
{
    public string lastScene;
    public void changeLastScene(string scene)
    {
        lastScene = scene;
    }
    static public void loadScene(string scene)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(scene);
    }
    public void loadLastScene()
    {
        loadScene(lastScene);
    }
}