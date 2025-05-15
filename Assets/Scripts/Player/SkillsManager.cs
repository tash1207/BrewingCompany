using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance { get; private set; }

    public int MaxGlasses = 5;
    public bool AllowRiskyPickup = false;
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
        if (level >= 1)
        {
            MaxGlasses = 6;
        }
        if (level >= 2)
        {
            AllowRiskyPickup = true;
        }
        if (level >= 3)
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
