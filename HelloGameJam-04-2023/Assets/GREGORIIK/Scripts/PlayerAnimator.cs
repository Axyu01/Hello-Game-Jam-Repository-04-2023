using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    bool isWalking;
    bool isRunning;

    Animator animator;
    PlayerHealth m_player;
    Rigidbody m_rigidbody;
    
    void Start()
    {
        m_player = FindObjectOfType<PlayerHealth>();
        m_rigidbody = m_player.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        isWalking = false;
        isRunning = false;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) { isWalking = false; isRunning = false; }
        else
        {
            if (m_rigidbody.velocity.magnitude > Mathf.Epsilon)
            {
                isWalking = true;
            }
            if (isWalking && Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
            }
            else isRunning = false;
        }

        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);
    }
}
