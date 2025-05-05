using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
            if (health != null) {
                health.TakeDamage(health.MaxHealth); // Player'ı öldür
            }
        }
    }
}
