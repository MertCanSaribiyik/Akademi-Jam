using UnityEngine.SceneManagement;

public static class SceneManagement
{
    public static void ReloadScene() {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
