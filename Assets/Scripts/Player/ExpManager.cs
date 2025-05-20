using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }
    
    public int Level { get; private set; }
    public int CurrentExp { get; private set; }
    public int ExpToNextLevel { get; private set; }
    private int expBetweenLevels = 12;

    private const string levelPref = "PlayerLevel";
    private const string expPref = "PlayerExp";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeValues();
    }

    void Start()
    {
        if (GameManager.Instance.IsStoryMode())
        {
            Actions.OnExpChanged(this);
        }
    }

    void OnEnable()
    {
        Actions.OnStartNewStory += ResetValues;
        Actions.OnGlasswareCleared += GainExperience;
        Actions.OnPoopsThrownAway += GainExperience;
        Actions.OnLevelEnded += SaveValues;
    }

    void OnDisable()
    {
        Actions.OnStartNewStory -= ResetValues;
        Actions.OnGlasswareCleared -= GainExperience;
        Actions.OnPoopsThrownAway -= GainExperience;
        Actions.OnLevelEnded -= SaveValues;
    }

    private void InitializeValues()
    {
        Level = PlayerPrefs.GetInt(levelPref, 0);
        CurrentExp = PlayerPrefs.GetInt(expPref, 0);
        ExpToNextLevel = (Level + 1) * expBetweenLevels;
    }

    public void GainExperience(int amount)
    {
        if (GameManager.Instance.IsStoryMode())
        {
            CurrentExp += amount;
            if (CurrentExp >= ExpToNextLevel)
            {
                LevelUp();
            }

            Actions.OnExpChanged(this);
        }
    }

    private void LevelUp()
    {
        Level++;
        CurrentExp -= ExpToNextLevel;
        ExpToNextLevel += ExpToNextLevel;
        Actions.OnLevelChanged(Level);
    }

    private void SaveValues()
    {
        PlayerPrefs.SetInt(levelPref, Level);
        PlayerPrefs.SetInt(expPref, CurrentExp);
    }

    private void ResetValues()
    {
        PlayerPrefs.SetInt(levelPref, 0);
        PlayerPrefs.SetInt(expPref, 0);
        InitializeValues();
    }
}
