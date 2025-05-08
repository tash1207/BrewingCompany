using System.Runtime.InteropServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject storyModeIntroDialog;
    [SerializeField] GameObject scoreModeIntroDialog;
    [SerializeField] GameOverDialog scoreModeGameOverDialog;
    [SerializeField] GameOverDialog storyModeDayOverDialog;

    // TODO: Probably move this logic into its own script.
    [SerializeField] GameObject mobileControlsDialog;
    [SerializeField] GameObject mobileControls;

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    private bool isStoryMode;

    void OnEnable()
    {
        Actions.OnLevelEnded += ShowGameOver;
    }

    void Start()
    {
        PauseControl.Instance.PauseGame();
        isStoryMode = GameManager.Instance.GameMode == GameManager.Mode.Story;

        if (!MaybeShowMobileControlsDialog())
        {
            ShowOpeningDialog();
        }
    }

    void OnDisable()
    {
        Actions.OnLevelEnded -= ShowGameOver;
    }

    /** Return whether the dialog is shown. */
    private bool MaybeShowMobileControlsDialog()
    {
        if (IsMobileWebGL() && !SettingsManager.Instance.GetMobileSetting())
        {
            mobileControlsDialog.SetActive(true);
            return true;
        }
        return false;;
    }

    private bool IsMobileWebGL()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
        #endif
        return false;
    }

    public void EnableMobileControls()
    {
        SettingsManager.Instance.SetMobileSetting(true);
        CloseMobileControlsDialog();
    }

    public void CloseMobileControlsDialog()
    {
        mobileControlsDialog.SetActive(false);
        ShowOpeningDialog();
    }

    private void ShowOpeningDialog()
    {
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
        PauseControl.Instance.ResumeGame();
        if (SettingsManager.Instance.GetMobileSetting())
        {
            mobileControls.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        Actions.ResetLevel();
        StartLevel();
    }

    public void NextLevel()
    {
        // TODO: Grab current day from settings.
        PlayerPrefs.SetInt("StoryCurrentDay", 1);
        GameManager.Instance.LoadStoryMode(1);
    }

    void ShowGameOver()
    {
        mobileControls.SetActive(false);

        if (isStoryMode)
        {
            storyModeDayOverDialog.Show();
        }
        else
        {
            scoreModeGameOverDialog.Show();
        }
    }

    public void SaveAndExitToMainMenu()
    {
        PlayerPrefs.SetInt("StoryCurrentDay", 1);
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
