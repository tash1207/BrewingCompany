using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] GameObject player;
    [SerializeField] GameObject storyModeDialog;
    [SerializeField] GameObject scoreModeDialog;
    [SerializeField] GameObject mobileControls;

    public Action OnChooseGreenShirt;
    public Action OnChooseYellowShirt;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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

    public void ChooseGreenShirt()
    {
        OnChooseGreenShirt?.Invoke();
    }

    public void ChooseYellowShirt()
    {
        OnChooseYellowShirt?.Invoke();
    }

    public void StartLevel()
    {
        if (SettingsManager.Instance.GetMobileSetting())
        {
            mobileControls.SetActive(true);
        }
        // Unpause game, set player active, start timer.
        player.SetActive(true);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
