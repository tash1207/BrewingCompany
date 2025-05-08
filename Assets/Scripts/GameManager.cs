using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Mode
    {
        Menu,
        Story,
        Score,
    }

    public static GameManager Instance { get; private set; }

    public Mode GameMode;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Initialize();
        DontDestroyOnLoad(gameObject);
    }

    void Initialize()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                GameMode = Mode.Menu;
                break;
            case 1:
                GameMode = Mode.Score;
                break;
        }
    }

    public void LoadMainMenu()
    {
        GameMode = Mode.Menu;
        SceneManager.LoadScene("Menu");
    }

    public void LoadStoryMode(int day)
    {
        GameMode = Mode.Story;
        if (day == 1)
        {
            SceneManager.LoadScene("Zone 2");
        }
        else
        {
            SceneManager.LoadScene("Zone 1");
        }
    }

    public void LoadZone1()
    {
        GameMode = Mode.Score;
        SceneManager.LoadScene("Zone 1");
    }

    public void LoadZone2()
    {
        GameMode = Mode.Score;
        SceneManager.LoadScene("Zone 2");
    }
}
