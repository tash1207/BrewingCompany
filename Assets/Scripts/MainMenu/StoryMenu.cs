using UnityEngine;
using UnityEngine.UI;

public class StoryMenu : MonoBehaviour
{
    [SerializeField] Button newStoryButton;
    [SerializeField] Button continueStoryButton;
    [SerializeField] GameObject overwriteAlert;

    private bool hasSavedStory;
    private int currentDay;

    // TODO: Don't use prefs for story mode save content.
    private const string storySettingName = "StoryCurrentDay";

    void Awake()
    {
        currentDay = PlayerPrefs.GetInt(storySettingName, -1);
        hasSavedStory = currentDay > 0;
    }

    void Start()
    {
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
        PlayerPrefs.SetInt(storySettingName, 0);
        currentDay = 0;
        LoadStoryMode();
    }

    void LoadStoryMode()
    {
        GameManager.Instance.LoadStoryMode(currentDay);
    }
}
