using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float damage = 5f;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 1f;
    [SerializeField] private LayerMask enemyLayer;

    //Set Attack keyframe  : 
    public void Attack() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayLength, enemyLayer);
        if (hit.collider != null) {
            HealthSystem healthSystem = hit.collider.GetComponent<HealthSystem>();
            if (healthSystem != null) {
                float attackDirection = transform.right.x;
                hit.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(5000f * attackDirection, 0f), ForceMode2D.Impulse);
                healthSystem.TakeDamage(damage);
            }
        }
    }

}