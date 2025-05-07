using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private int currentScore = 0;
    private int glasswareValue = 1;
    private int poopValue = 4;

    void OnEnable()
    {
        scoreText.text = currentScore.ToString();
        Actions.OnGlasswareCleared += IncrementGlasswareScore;
        Actions.OnPoopsThrownAway += IncrementPoopThrownAwayScore;
    }

    void OnDisable()
    {
        Actions.OnGlasswareCleared -= IncrementGlasswareScore;
        Actions.OnPoopsThrownAway -= IncrementPoopThrownAwayScore;
    }

    void IncrementGlasswareScore(int glasswareAmount)
    {
        IncrementScore(glasswareAmount * glasswareValue);
    }

    void IncrementPoopThrownAwayScore(int poopAmount)
    {
        IncrementScore(poopAmount * poopValue);
    }

    void IncrementScore(int amount)
    {
        currentScore += amount;
        scoreText.text = currentScore.ToString();
    }
}
