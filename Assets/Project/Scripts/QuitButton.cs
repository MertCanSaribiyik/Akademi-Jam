using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button button;

    private void Start() {
        button = GetComponent<Button>();

        button.onClick.AddListener(Quit);
    }

    private void OnDestroy() {
        button.onClick.RemoveListener(Quit);
    }

    public void Quit() {
        print("Quit Game");
        Application.Quit();
    }
}