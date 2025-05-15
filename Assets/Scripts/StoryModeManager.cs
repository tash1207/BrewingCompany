using UnityEngine;

public class StoryModeManager : MonoBehaviour
{
    public static StoryModeManager Instance { get; private set; }

    public bool HasSavedStory { get; private set; }
    public int CurrentDay { get; private set; }

    public int MinimumScoreToNotFailDay = 5;

    // TODO: Don't use prefs for story mode save content.
    private const string currentDaySettingName = "StoryCurrentDay";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        GrabValues();
    }

    void OnEnable()
    {
        Actions.OnStartNewStory += InitialStorySave;
    }

    void OnDisable()
    {
        Actions.OnStartNewStory -= InitialStorySave;
    }

    private void GrabValues()
    {
        CurrentDay = PlayerPrefs.GetInt(currentDaySettingName, -1);
        HasSavedStory = CurrentDay > 1;
    }

    public void InitialStorySave()
    {
        PlayerPrefs.SetInt(currentDaySettingName, 1);
        GrabValues();
    }

    public void IncrementCurrentDay()
    {
        CurrentDay++;
        PlayerPrefs.SetInt(currentDaySettingName, CurrentDay);
        GrabValues();
    }
}
