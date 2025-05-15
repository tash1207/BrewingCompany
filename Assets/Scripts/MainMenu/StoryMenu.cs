using UnityEngine;
using UnityEngine.UI;

public class StoryMenu : MonoBehaviour
{
    [SerializeField] Button newStoryButton;
    [SerializeField] Button continueStoryButton;
    [SerializeField] GameObject overwriteAlert;

    private bool hasSavedStory;
    private int currentDay;

    void Start()
    {
        currentDay = StoryModeManager.Instance.CurrentDay;
        hasSavedStory = StoryModeManager.Instance.HasSavedStory;

        SetUpButtons();
    }

    void SetUpButtons()
    {
        continueStoryButton.interactable = hasSavedStory;
    }

    public void NewStory()
    {
        if (hasSavedStory)
        {
            overwriteAlert.SetActive(true);
        }
        else
        {
            Actions.OnStartNewStory();
            LoadStoryMode();
        }
    }

    public void ContinueStory()
    {
        LoadStoryMode();
    }

    public void ConfirmNewStory()
    {
        // Overwrite old story.
        Actions.OnStartNewStory();
        currentDay = StoryModeManager.Instance.CurrentDay;
        LoadStoryMode();
    }

    void LoadStoryMode()
    {
        GameManager.Instance.LoadStoryMode(currentDay);
    }
}
