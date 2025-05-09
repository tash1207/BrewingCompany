using UnityEngine;

public class StoryModeManager : MonoBehaviour
{
    public static StoryModeManager Instance { get; private set; }

    public bool HasSavedStory { get; private set; }
    public int CurrentDay { get; private set; }

    // TODO: Don't use prefs for story mode save content.
    private const string storySettingName = "StoryCurrentDay";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GrabValues();
    }

    private void GrabValues()
    {
        CurrentDay = PlayerPrefs.GetInt(storySettingName, -1);
        HasSavedStory = CurrentDay > 0;
    }

    public void InitialStorySave()
    {
        PlayerPrefs.SetInt(storySettingName, 0);
        GrabValues();
    }

    public void IncrementCurrentDay()
    {
        CurrentDay++;
        PlayerPrefs.SetInt(storySettingName, CurrentDay);
        GrabValues();
    }
}
