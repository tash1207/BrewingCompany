using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static PauseControl Instance { get; private set; }

    public bool GameIsPaused { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PauseGame()
    {
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        GameIsPaused = false;
    }
}
