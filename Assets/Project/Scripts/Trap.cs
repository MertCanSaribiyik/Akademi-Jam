using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            ContactPoint2D contactPoint = collision.GetContact(0);

            if(contactPoint.normal.y > .5f) {
                print("Not contact");
                return;
            }

            HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
            if (health != null) {
                health.TakeDamage(health.MaxHealth); // Player'ı öldür
            }
        }
    }
}
