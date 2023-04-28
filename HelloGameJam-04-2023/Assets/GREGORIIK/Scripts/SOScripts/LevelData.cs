using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelData", menuName = "Player Data/LevelData")]
public class LevelData : ScriptableObject
{
    public string lastScene;
    public void changeLastScene(string scene)
    {
        lastScene= scene;
    }
    static public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void loadLastScene()
    {
        loadScene(lastScene);
    }
}
