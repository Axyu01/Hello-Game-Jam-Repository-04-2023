using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneVolume : MonoBehaviour
{
    [SerializeField]
    LevelData levelData;
    [SerializeField]
    VolumeType volumeType;
    [SerializeField]
    string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (volumeType == VolumeType.Death)
        {
            LevelData.loadScene("DeathScreen");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (volumeType == VolumeType.NextLevel)
        {
            levelData.changeLastScene(sceneName);
            levelData.loadLastScene();
        }
        else if (volumeType == VolumeType.NormalScene)
        {
            LevelData.loadScene(sceneName);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public enum VolumeType
    {
        NextLevel,Death,NormalScene
    }
}
