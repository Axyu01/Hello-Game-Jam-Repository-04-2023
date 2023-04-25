using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : InteractableObject
{
    [SerializeField]
    ChangeTransform changeTransform;
    [SerializeField]
    AudioSource audioSource;
    public override void onInteraction()
    {
        base.onInteraction();
        if(changeTransform!=null)
            changeTransform.onInteraction();
        if(audioSource!=null)
            audioSource.Play();
    }
}
