using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucjanAnimationAndRotation : MonoBehaviour
{
    [SerializeField] float focusPointHeight = 0f;
    [SerializeField] float amplitude = 2f;
    [SerializeField] float timeMultiplier = 0.2f;

    PlayerHealth m_player;
    Vector3 target;
    Vector3 m_position;

    void Start()
    {
        m_player = FindObjectOfType<PlayerHealth>();
        m_position = gameObject.transform.position;
    }

    void Update()
    {
        UpAndDownMovement();
        RotateTowardsPlayer();
    }

    public void RotateTowardsPlayer()
    {
        target = m_player.gameObject.transform.position;
        target = new Vector3(m_player.gameObject.transform.position.x, focusPointHeight, m_player.gameObject.transform.position.z);
        gameObject.transform.LookAt(target);
    }

    private void UpAndDownMovement()
    {
        float newPos = Mathf.PingPong(Time.time * timeMultiplier, amplitude);
        m_position.y = newPos;
        gameObject.transform.position = m_position;
    }
}
