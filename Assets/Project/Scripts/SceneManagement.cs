using UnityEngine.SceneManagement;

public static class SceneManagement
{
    public static void ReloadScene() {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void NextScene() {
        // Load the next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
