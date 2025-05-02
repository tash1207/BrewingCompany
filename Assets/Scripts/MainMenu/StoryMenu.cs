using UnityEngine;
using UnityEngine.UI;

public class StoryMenu : MonoBehaviour
{
    [SerializeField] Button newStoryButton;
    [SerializeField] Button continueStoryButton;
    [SerializeField] GameObject overwriteAlert;

    private bool hasSavedStory;

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
        LoadStoryMode();
    }

    void LoadStoryMode()
    {
        GameManager.Instance.LoadStoryMode();
    }
}
