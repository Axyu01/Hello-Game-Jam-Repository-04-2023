using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : Singleton<BackgroundMusic>
{
    AudioSource m_aSource;
    [SerializeField] AudioClip[] backgroundMusicList;

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
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.N)) { m_aSource.clip = backgroundMusicList[1]; m_aSource.Play();  }
    }

    /*public void PlayNextBackgroundMusic()
    {
        m_aSource.clip = backgroundMusicList[0];
        m_aSource.Play();
    }*/

}
