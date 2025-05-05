using UnityEngine;

public class WakeUpBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossUIPanel;

    [SerializeField] private GameObject speechBubble;

    private void Start() {
        boss.SetActive(false);
        bossUIPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            speechBubble.SetActive(true);
            boss.SetActive(true);
            bossUIPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}