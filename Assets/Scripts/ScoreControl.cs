using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public static ScoreControl Instance { get; private set; }
    public int CurrentScore = 0;

    [SerializeField] TMP_Text scoreText;

    private int glasswareValue = 1;
    private int poopValue = 4;

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
        scoreText.text = CurrentScore.ToString();
        Actions.OnGlasswareCleared += IncrementGlasswareScore;
        Actions.OnPoopsThrownAway += IncrementPoopThrownAwayScore;
        Actions.ResetLevel += ResetScore;
    }

    void OnDisable()
    {
        Actions.OnGlasswareCleared -= IncrementGlasswareScore;
        Actions.OnPoopsThrownAway -= IncrementPoopThrownAwayScore;
        Actions.ResetLevel -= ResetScore;
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
        CurrentScore += amount;
        scoreText.text = CurrentScore.ToString();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        scoreText.text = CurrentScore.ToString();
    }
}
