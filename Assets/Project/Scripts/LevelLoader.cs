using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private Animator transition;
    [SerializeField] private float transitionDelay = 1f;

    private void Awake() {
        transition = GetComponent<Animator>();
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel() {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionDelay);
        SceneManagement.NextScene();
    }
}