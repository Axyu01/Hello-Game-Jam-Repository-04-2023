using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float timeFromLastDamage = 0;
    [SerializeField] float healthRestoreDelay = 5;
    [SerializeField] float healthRestoreAmount = 0.5f;
    [SerializeField] float damageFromEnemy = 25;


    private bool isDead;
    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public float currentHealth;

    LevelManager levelManager;

    void Start()
    {
        currentHealth = MaxHealth;
        //levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    }

    void Update()
    {
        if (timeFromLastDamage >= healthRestoreDelay && currentHealth < MaxHealth)
        {
            currentHealth += healthRestoreAmount;
            if(currentHealth > MaxHealth) currentHealth= MaxHealth;
        }

        if (timeFromLastDamage < healthRestoreDelay) timeFromLastDamage += Time.deltaTime;
        //if (currentHealth <= 0) LevelData.loadScene("DeathScreen");//levelManager.OnPlayerDeath();

        if (currentHealth <= 0)
        {
            isDead = true; 
            deathScreenController.DeathHandler();
        }
        if (isDead) { deathScreenController.DeathHandler(); }
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
