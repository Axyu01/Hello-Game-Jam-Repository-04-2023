using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathArea : MonoBehaviour
{
    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth.IsDead = true;
        }
    }
}
