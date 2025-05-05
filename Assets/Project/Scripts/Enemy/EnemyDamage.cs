using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damage = 5f;

    [Header("BoxCast Settings")]
    [SerializeField] private Vector2 castSize = new Vector2(1.5f, 1f);
    [SerializeField] private float castDistance = 1f;
    [SerializeField] private Vector2 castOffset = Vector2.zero;
    [SerializeField] private LayerMask targetLayer;

    private EnemyAI enemyAI;

    private void Awake() {
        enemyAI = GetComponent<EnemyAI>();
    }

    // Called via animation event
    public void PerformAttack() {
        if (enemyAI == null || enemyAI.IsPlayerDead) return;

        Vector2 origin = (Vector2)transform.position + castOffset;

        RaycastHit2D hit = Physics2D.BoxCast(
            origin,
            castSize,
            0f,
            transform.right,
            castDistance,
            targetLayer
        );

        if (hit.collider != null) {
            HealthSystem health = hit.collider.GetComponent<HealthSystem>();
            if (health != null) {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + (Vector3)castOffset + transform.right * (castDistance / 2f);
        Gizmos.DrawWireCube(origin, castSize);
    }
}
