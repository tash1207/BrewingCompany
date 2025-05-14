using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    public int MaxGlasses = 5;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void OnEnable()
    {
        Actions.OnLevelChanged += LevelChanged;
    }

    void OnDisable()
    {
        Actions.OnLevelChanged -= LevelChanged;
    }

    private void LevelChanged(int level)
    {
        // TODO: Figure out what the stats to level relationship is.
        if (level >= 1)
        {
            MaxGlasses = 6;
        }
    }
}
