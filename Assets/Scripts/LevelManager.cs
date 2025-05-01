using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject storyModeDialog;
    [SerializeField] GameObject scoreModeDialog;
    [SerializeField] GameObject mobileControls;

    void Start()
    {
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

    public void ChooseGreenShirt()
    {
        Actions.OnChooseShirt(PlayerDisplayOptions.Shirt.Green);
    }

    public void ChooseYellowShirt()
    {
        Actions.OnChooseShirt(PlayerDisplayOptions.Shirt.Yellow);
    }

    public void StartLevel()
    {
        Actions.OnLevelStarted();
        if (SettingsManager.Instance.GetMobileSetting())
        {
            mobileControls.SetActive(true);
        }
        // Unpause game, start timer.
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
