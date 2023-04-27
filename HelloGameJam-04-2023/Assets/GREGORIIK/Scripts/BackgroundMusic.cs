using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : Singleton<BackgroundMusic>
{
    AudioSource m_aSource;
    AudioLoader audioLoader;
    [SerializeField] public AudioClip[] backgroundMusicList;
    //AudioClip backgroundMusic;

    public int currentSceneIndex;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        m_aSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

}
