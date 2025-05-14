using TMPro;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager Instance { get; private set; }

    [SerializeField] GameObject levelUpCanvas;
    [SerializeField] TMP_Text newSkillsText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowLevelUpDialog(int level)
    {
        PauseControl.Instance.PauseGame();
        string text = "No new skill acquired for this level but congrats anyways! :)";
        switch (level)
        {
            case 1:
                text = "Max glassware increased to " + StatsManager.Instance.MaxGlasses + ".";
                break;
            case 2:
                text = "Ability to attempt to pick up more glasses than max glassware " +
                    " (at risk of dropping all of them).";
                break;
            case 3:
                //"Lower chance of dropping all glasses when attempting to carry more than max."
                break;
            default:
                break;
        }

        newSkillsText.text = text;
        levelUpCanvas.SetActive(true);
    }
}
