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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameMode = Mode.Menu;
        }
        else
        {
            GameMode = Mode.Score;
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
        if (day == 2)
        {
            SceneManager.LoadScene("Zone 2");
        }
        else if (day >= 3)
        {
            SceneManager.LoadScene("Zones 1&2");
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

    public void LoadZones1and2()
    {
        GameMode = Mode.Score;
        SceneManager.LoadScene("Zones 1&2");
    }

    public bool IsStoryMode()
    {
        return GameMode == Mode.Story;
    }
}
