using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioLoader : Singleton<AudioLoader>
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        audioSource = FindObjectOfType<AudioSource>();
    }
    /*
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }*/

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;
        if (sceneIndex >= 0 && sceneIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[sceneIndex];
            audioSource.Play();
        }
    }
}