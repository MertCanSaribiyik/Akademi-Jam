using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private Slider healthBar;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth = 100f;

    private void Awake() {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        currentHealth = healthBar.value;
    }

    public void TakeDamange(float damage) {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0) {
            //TODO : DEATH
        }
    }





}