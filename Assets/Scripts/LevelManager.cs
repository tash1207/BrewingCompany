using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject storyModeIntroDialog;
    [SerializeField] GameObject scoreModeIntroDialog;
    [SerializeField] GameObject scoreModeGameOverDialog;

    // TODO: Probably move this logic into its own script.
    [SerializeField] GameObject mobileControls;

    private bool isStoryMode;

    void OnEnable()
    {
        Actions.OnLevelEnded += ShowGameOver;
    }

    void Start()
    {
        isStoryMode = GameManager.Instance.GameMode == GameManager.Mode.Story;


        if (isStoryMode)
        {
            // Show dialog to choose shirt.
            storyModeIntroDialog.SetActive(true);
        }
        else
        {
            // Show instructions dialog and button to start level.
            scoreModeIntroDialog.SetActive(true);
        }
    }

    void OnDisable()
    {
        Actions.OnLevelEnded -= ShowGameOver;
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
    }

    void ShowGameOver()
    {
        if (!isStoryMode)
        {
            scoreModeGameOverDialog.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
