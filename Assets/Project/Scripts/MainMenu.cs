using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator bookAnimator;

    [SerializeField] private GameObject first;
    [SerializeField] private GameObject second;

    [SerializeField] private float delay = 2f;
    [SerializeField] private float delay2 = 4f;

    private bool isAnimate = true;

    private void Awake() {
        first.SetActive(false);
        second.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (isAnimate) {
                isAnimate = false;
                bookAnimator.SetTrigger("play");
            }
        }
    }

    public void PlayBtn() {
        AudioManager.Instance.PlayOneShot(SoundEffectType.Button);
        bookAnimator.SetTrigger("close");
        StartCoroutine(Enumerator());

    }

    public void QuitBtn() {
        AudioManager.Instance.PlayOneShot(SoundEffectType.Button);
        Application.Quit();
    }

    private IEnumerator Enumerator() {
        yield return new WaitForSeconds(delay);
        first.SetActive(true);
        yield return new WaitForSeconds(delay2);
        second.SetActive(true);
        yield return new WaitForSeconds(delay2);
        SceneManagement.NextScene();

    }


}