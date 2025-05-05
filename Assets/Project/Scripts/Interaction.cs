using System.Collections;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject tutorialDialogue;
    [SerializeField] private float delay = 1.5f;

    private void Awake() {
        if (tutorialDialogue != null) {
            tutorialDialogue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            StartCoroutine(OpenBubble(collision.GetComponent<PlayerController>().SpeechBubble));
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().SpeechBubble.SetActive(false);
            tutorialDialogue.SetActive(false);
        }
    }

    private IEnumerator OpenBubble(GameObject bubble) {
        bubble.SetActive(true);
        yield return new WaitForSeconds(delay);
        tutorialDialogue.SetActive(true);
    }
}
