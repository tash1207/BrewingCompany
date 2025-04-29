using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject storyModeDialog;
    [SerializeField] GameObject scoreModeDialog;

    void Start()
    {
        player.SetActive(false);
        Time.timeScale = 0;

        if (GameManager.Instance.GameMode == GameManager.Mode.Story)
        {
            // Show dialog to choose shirt.
            storyModeDialog.SetActive(true);
        }
        else if (GameManager.Instance.GameMode == GameManager.Mode.Score)
        {
            // Show instructions dialog and button to start level.
            scoreModeDialog.SetActive(true);
        }
    }

    public void ChooseShirt()
    {
        // TODO: Implement.
    }

    public void StartLevel()
    {
        // Unpause game, set player active, start timer.
        player.SetActive(true);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
