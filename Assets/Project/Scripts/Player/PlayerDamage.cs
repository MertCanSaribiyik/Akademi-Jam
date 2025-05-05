using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float damage = 5f;
    [SerializeField] private float forceAmount = 10f;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 1f;
    [SerializeField] private LayerMask enemyLayer;

    private bool isEnemyDeath = false;

    private void Start() {
        HealthSystem.OnEnemyDeath += OnEnemyDeath;
    }

    private void OnDestroy() {
        HealthSystem.OnEnemyDeath -= OnEnemyDeath;
    }
    private void OnEnemyDeath() {
        isEnemyDeath = true;
    }

    // Bu method animasyonun attack keyframe'ine bağlanmalı
    public void Attack() {
        //if (isEnemyDeath) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayLength, enemyLayer);

        if (hit.collider != null) {
            // Hasar verme işlemi
            HealthSystem healthSystem = hit.collider.GetComponent<HealthSystem>();
            if (healthSystem != null) {
                healthSystem.TakeDamage(damage);
            }

            // Geri itme işlemi
            Rigidbody2D enemyRb = hit.collider.GetComponent<Rigidbody2D>();

            if (enemyRb != null && !hit.collider.CompareTag("Boss")) {
                // Yönü oyuncudan düşmana doğru ayarla
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                Vector2 knockback = new Vector2(direction.x * forceAmount * enemyRb.mass, 0f); // sadece yatay kuvvet
                enemyRb.AddForce(knockback, ForceMode2D.Impulse);
            }
        }
    }

    // Editor'de ray'i göstermek için
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * rayLength);
    }
}
