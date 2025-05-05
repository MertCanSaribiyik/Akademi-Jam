using UnityEngine;

public class GateScene : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject speechbubble;

    [SerializeField] private float delay = 1f;

    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private bool isLastScene = false;
    [SerializeField] private GameObject lastMessage;

    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private AudioClip finishMusic;

    private void Start() {
        speechbubble.SetActive(false);

        if(lastMessage != null) {
            lastMessage.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (playerInventory.hasKey) {
                playerInventory.hasKey = false;

                if (isLastScene) {
                    lastMessage.SetActive(true);
                    AudioManager.Instance.PlayMusic(finishMusic);
                    playerHealthBar.SetActive(false);
                    bossHealthBar.SetActive(false);
                }

                else {
                    levelLoader.LoadNextLevel();
                }
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
