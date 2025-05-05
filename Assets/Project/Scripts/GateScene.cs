using UnityEngine;

public class GateScene : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject speechbubble;

    [SerializeField] private float delay = 1f;

    [SerializeField] private LevelLoader levelLoader;


    private void Start() {
        speechbubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (playerInventory.hasKey) {
                playerInventory.hasKey = false;
                levelLoader.LoadNextLevel();
            }

            else {
                speechbubble.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            speechbubble.SetActive(false);
        }
    }

}
