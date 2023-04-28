using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSFXPlayer : MonoBehaviour
{
    //Dorobiæ wy³¹czanie przy podskoczeniu 


    GameObject player;
    AudioSource audioSource;

    private Rigidbody playerRigidbody;
    private bool isMoving;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        audioSource = GetComponent<AudioSource>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        isMoving = false;
    }

    void Update()
    {
        Vector3 currentPlayerVelocity = playerRigidbody.velocity;

        if (currentPlayerVelocity.magnitude > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        gameObject.transform.position = player.transform.position;

        PlayMovementSFX();
    }

    void PlayMovementSFX()
    {
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}