using UnityEngine;

public class VisibiltyController : MonoBehaviour
{
    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}
