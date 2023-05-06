using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class StartAudioVolume : MonoBehaviour
{
    [SerializeField]
    VolumeType volumeType;
    AudioSource audioSource;
    MainMenuUIController mainMenuUIController;

    // Start is called before the first frame update
    bool playFlag = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (playFlag == false)
            return;
        if (volumeType == VolumeType.Narration)
        { 
            var temp = other.GetComponent<AudioSource>();
            temp.clip=audioSource.clip;
            temp.loop = false;
            temp.Play();
            Destroy(gameObject);
        }
        if(volumeType==VolumeType.OneTime)
        {
            audioSource.loop = false;
            audioSource.Play();
            playFlag= false;
        }
        if(volumeType == VolumeType.EnterRepeatable)
        {
            audioSource.loop = false;
            audioSource.Play();
        }
    }
    [SerializeField]
    public enum VolumeType
    {
        OneTime,EnterRepeatable,Narration
    }
}
