using TMPro;
using UnityEngine;

public class TimerControl : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private float levelDuration = 60f;
    private float timeLeft;
    private bool timerOn;

    void OnEnable()
    {
        Actions.OnLevelStarted += StartTimer;
    }

    void OnDisable()
    {
        Actions.OnLevelStarted -= StartTimer;
    }

    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimerUI(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                Actions.OnLevelEnded();
            }
        }
    }

    void UpdateTimerUI(float currentTime)
    {
        // Since we round the float down, add 1 so the timer UI matches the real countdown.
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StartTimer()
    {
        timeLeft = levelDuration;
        timerOn = true;
    }
}
