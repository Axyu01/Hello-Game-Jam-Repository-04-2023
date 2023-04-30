using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] float alpha = 0;
    Color color;
    float m_health;

    void Start()
    {
        m_health = playerHealth.currentHealth;
        color = image.color;
    }


    void Update()
    {
        m_health = playerHealth.currentHealth;
        color.a = 1-m_health/100;
        image.color = new Color(color.r, color.g, color.b, color.a);
    }
}
