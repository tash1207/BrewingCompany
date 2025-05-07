using System.Runtime.InteropServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject storyModeIntroDialog;
    [SerializeField] GameObject scoreModeIntroDialog;
    [SerializeField] GameObject scoreModeGameOverDialog;

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
        // TODO Check if on mobile.
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
