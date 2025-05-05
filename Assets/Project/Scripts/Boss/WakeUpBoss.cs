using UnityEngine;

public class WakeUpBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossUIPanel;

    [SerializeField] private GameObject speechBubble;

    [SerializeField] private AudioClip boosClip;

    private void Start() {
        boss.SetActive(false);
        bossUIPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            AudioManager.Instance.PlayMusic(boosClip);
            speechBubble.SetActive(true);
            boss.SetActive(true);
            bossUIPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}