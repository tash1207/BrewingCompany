using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDialog : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text statusText;

    [SerializeField] Button menuButton;
    [SerializeField] Button failedMenuButton;
    [SerializeField] Button continueButton;

    public void Show()
    {
        int score = ScoreControl.Instance.CurrentScore;
        scoreText.text = "Score : " + score;
        if (GameManager.Instance.GameMode == GameManager.Mode.Story)
        {
            SetStatusText(score);
        }
        gameObject.SetActive(true);
    }

    private void SetStatusText(int score)
    {
        if (StoryModeManager.Instance.CurrentDay >= 3)
        {
            continueButton.interactable = false;
            statusText.text = "You've reached the end of the demo!";
        }
        else
        {
            statusText.text = GetStatusTextForScore(score);
        }
    }

    private string GetStatusTextForScore(int score)
    {
        if (score < StoryModeManager.Instance.MinimumScoreToNotFailDay)
        {
            failedMenuButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(false);
            continueButton.interactable = false;
            return "That was terrible. You're fired.";
        }

        if (score >= GetScoreThresholdForGreatJob())
        {
            return GetTextForGreatJob();
        }
        else if (score >= GetScoreThresholdForGoodJob())
        {
            return GetTextForGoodJob();
        }
        else
        {
            return "Good job today, but we'd like to see you speed up a bit.";
        }
    }

    private int GetScoreThresholdForGreatJob()
    {
        switch (StoryModeManager.Instance.CurrentDay)
        {
            case 1:
                return 23;
            case 2:
                return 31;
            case 3:
                return 50;
            default:
                return 23;
        }
    }

    private int GetScoreThresholdForGoodJob()
    {
        switch (StoryModeManager.Instance.CurrentDay)
        {
            case 1:
                return 11;
            case 2:
                return 20;
            case 3:
                return 38;
            default:
                return 11;
        }
    }

    private string GetTextForGreatJob()
    {
        switch (StoryModeManager.Instance.CurrentDay)
        {
            case 1:
                return "Wow! Amazing work today! Thanks for picking up the dog poop.";
            case 2:
                return "Wow! Amazing work today!";
            case 3:
                return "Wow! Amazing work today!";
            default:
                return "Wow! Amazing work today!";
        }
    }

    private string GetTextForGoodJob()
    {
        switch (StoryModeManager.Instance.CurrentDay)
        {
            case 1:
                return "Solid first day! See you tomorrow!";
            case 2:
                return "Solid work today! See you tomorrow!";
            case 3:
                return "Solid work today!";
            default:
                return "Solid work today!";
        }
    }
}
