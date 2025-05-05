using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private bool isPlayer = true; // Set to true for player, false for enemy
    [SerializeField] private bool isBoss = false;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth = 100f;

    [SerializeField] private Animator bossAnimator;

    [SerializeField] private PlayerInventory playerInventory;

    //events : 
    public static event Action OnPlayerDeath = delegate { };
    public static event Action OnEnemyDeath = delegate { };
    public static event Action OnBossDeath = delegate { };

    //Getters :
    public float MaxHealth { get { return maxHealth; } }

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

        if(isPlayer) {
            AudioManager.Instance.PlayOneShot(SoundEffectType.PlayerDamage);
        }
    }

    private void Die() {
        if(isPlayer) {
            animator.SetTrigger("death");
            playerInventory.hasKey = false;

            GetComponent<PlayerController>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            OnPlayerDeath.Invoke();
            StartCoroutine(GameOver());
        }
        else if(isBoss) {
            AudioManager.Instance.PlayOneShot(SoundEffectType.BoosDeath);
            bossAnimator.SetTrigger("death");
            OnBossDeath.Invoke();
            StartCoroutine(BossDeath());
        }

        else {
            Debug.Log("Enemy has died.");
            Destroy(gameObject);
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

    private IEnumerator BossDeath() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
