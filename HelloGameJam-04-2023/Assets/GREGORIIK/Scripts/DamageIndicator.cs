using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] PlayerHealth playerHealth;
    Color color;
    float m_health;

    void Start()
    {
        m_health = playerHealth.currentHealth;
        
    }


    void Update()
    {
        m_health = playerHealth.currentHealth;
        color.a = 255 - m_health*2.5f;
    }
}
