using UnityEngine;
using UnityEngine.EventSystems;

public class PauseControl : MonoBehaviour
{
    public static PauseControl Instance { get; private set; }

    public bool GameIsPaused { get; private set; }

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject resumeButton;

    private bool showingPauseMenu;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void OnEnable()
    {
        Actions.TogglePauseMenu += OnTogglePauseMenu;
    }

    void OnDisable()
    {
        Actions.TogglePauseMenu -= OnTogglePauseMenu;
    }

    void OnTogglePauseMenu()
    {
        if (GameIsPaused && !showingPauseMenu) { return; }

        if (showingPauseMenu)
            HidePauseMenu();
        else
            ShowPauseMenu();
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(resumeButton);
        showingPauseMenu = true;
        PauseGame();
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        showingPauseMenu = false;
        ResumeGame();
    }

    public void PauseGame()
    {
        GameIsPaused = true;
        Time.timeScale = 0;
        Actions.OnLevelPaused();
    }

    public void ResumeGame()
    {
        GameIsPaused = false;
        Time.timeScale = 1;
        Actions.OnLevelResumed();
    }
}
