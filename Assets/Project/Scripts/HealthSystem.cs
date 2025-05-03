using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private bool isPlayer = true; // Set to true for player, false for enemy

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth = 100f;

    //events : 
    public static event Action OnPlayerDeath = delegate { };
    public static event Action OnEnemyDeath = delegate { };

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        currentHealth = healthBar.value;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        if(isPlayer) {
            animator.SetTrigger("death");

            GetComponent<PlayerController>().enabled = false;

            OnPlayerDeath.Invoke();
            StartCoroutine(GameOver());
        }
        else {
            Debug.Log("Enemy has died.");
            gameObject.SetActive(false);
            OnEnemyDeath.Invoke();
        }
    }

    #region TODO : HEAL
    public void Heal(float healAmount) {
        currentHealth += healAmount;
        healthBar.value = currentHealth;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void SetMaxHealth(float newMaxHealth) {
        maxHealth = newMaxHealth;
        healthBar.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
    }
    #endregion

    public IEnumerator GameOver() {
        yield return new WaitForSeconds(2f);
        SceneManagement.ReloadScene();
    }
}
