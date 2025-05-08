using TMPro;
using UnityEngine;

public class GameOverDialog : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void Show()
    {
        scoreText.text = "Score : " + ScoreControl.Instance.CurrentScore;
        gameObject.SetActive(true);
    }
}
