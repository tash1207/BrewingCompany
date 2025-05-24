using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance { get; private set; }

    public int MaxGlasses = 5;
    public bool AllowRiskyPickup = true;
    public bool AllowBusTubPickup = false;

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
        if (GameManager.Instance.IsStoryMode())
        {
            SetStatsForLevel(ExpManager.Instance.Level);
        }
    }

    void OnEnable()
    {
        Actions.OnLevelChanged += LevelChanged;
    }

    void OnDisable()
    {
        Actions.OnLevelChanged -= LevelChanged;
    }

    private void SetStatsForLevel(int level)
    {
        AllowRiskyPickup = true;
        if (level >= 1)
        {
            MaxGlasses = 7;
            Actions.OnMaxGlasswareChanged();
        }
        // if (level >= 2)
        // {
        //     AllowRiskyPickup = true;
        // }
        if (level >= 2)
        {
            AllowBusTubPickup = true;
        }
    }

    private void LevelChanged(int level)
    {
        SetStatsForLevel(level);
        ShowLevelUpDialog(level);
    }

    private void ShowLevelUpDialog(int level)
    {
        LevelUpManager.Instance.ShowLevelUpDialog(level);
    }
}
