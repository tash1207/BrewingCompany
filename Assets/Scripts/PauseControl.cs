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
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        GameIsPaused = false;
        Time.timeScale = 1;
    }
}
