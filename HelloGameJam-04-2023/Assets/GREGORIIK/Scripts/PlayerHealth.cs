using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float timeFromLastDamage = 0;
    [SerializeField] float healthRestoreDelay = 5;
    [SerializeField] float healthRestoreAmount = 0.5f;
    [SerializeField] float damageFromEnemy = 25;
    public float currentHealth;

    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        currentHealth = MaxHealth;
    }

    void Update()
    {
        if (timeFromLastDamage >= healthRestoreDelay && currentHealth < MaxHealth)
        {
            currentHealth += healthRestoreAmount;
            if(currentHealth > MaxHealth) currentHealth= MaxHealth;
        }

        if (timeFromLastDamage < healthRestoreDelay) timeFromLastDamage += Time.deltaTime;
        if (currentHealth <= 0) levelManager.OnPlayerDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            StartCoroutine(DamageDealer());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            StopAllCoroutines();
        }

    }

    public void ResetPlayerHealth()
    {
        currentHealth = maxHealth;
    }

    IEnumerator DamageDealer()
    {
        while (true) { 
            timeFromLastDamage = 0;
            currentHealth -= damageFromEnemy;
            yield return new WaitForSeconds(1); 
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        private set { MaxHealth = value; }
    }

}
