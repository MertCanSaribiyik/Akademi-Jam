using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 5f;

    [SerializeField] private float attackCooldown = 1f;
    private float time = 0f;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 1f;
    [SerializeField] private LayerMask playerLayer;

    private void Awake() {
        time = attackCooldown;
    }

    private void Update() {
        if(!GetComponent<EnemyAI>().IsPlayerDead) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, rayLength, playerLayer);
            //Debug.Log("Ray hit: " + hit.collider?.name);
            if (hit.collider != null && hit.collider.CompareTag("Player")) {
                HealthSystem healthSystem = hit.collider.GetComponent<HealthSystem>();
                if (healthSystem != null && time <= 0f) {
                    time = attackCooldown;
                    healthSystem.TakeDamage(damage);
                }
                else {
                    time -= Time.deltaTime;
                }
            }
        }

    }

}