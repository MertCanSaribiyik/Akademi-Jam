using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerInventory.hasKey = true;
            Destroy(gameObject);
        }
    }
}