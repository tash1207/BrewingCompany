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
        // TODO: Set different threshholds for each level.
        if (score > 23)
        {
            return "Wow! Amazing work today! Thanks for picking up the dog poop.";
        }
        else if (score > 11)
        {
            return "Solid work today! See you tomorrow!";
        }
        else
        {
            return "Good job today, but we'd like to see you speed up a bit.";
        }
    }
}
