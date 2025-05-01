using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private int currentScore = 0;
    private int glasswareValue = 1;

    void OnEnable()
    {
        scoreText.text = currentScore.ToString();
        Actions.OnGlasswareCleared += IncrementGlasswareScore;
    }

    void OnDisable()
    {
        Actions.OnGlasswareCleared -= IncrementGlasswareScore;
    }

    void IncrementGlasswareScore(int glasswareAmount)
    {
        IncrementScore(glasswareAmount * glasswareValue);
    }

    void IncrementScore(int amount)
    {
        currentScore += amount;
        scoreText.text = currentScore.ToString();
    }
}
