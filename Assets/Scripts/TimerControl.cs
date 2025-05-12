using TMPro;
using UnityEngine;

public class TimerControl : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float levelDuration = 60f;
    [SerializeField] float defaultBeepTime = 5f;

    private float beepTime;
    private float timeLeft;
    private bool timerOn;

    void Start()
    {
        UpdateTimerUI(levelDuration - 1);
    }

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

                if (timeLeft < beepTime)
                {
                    FinalSeconds();
                }
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                PauseControl.Instance.PauseGame();
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

    void FinalSeconds()
    {
        timerText.color = Color.red;
        SFXManager.Instance.PlayTimerBeep();
        beepTime -= 1f;
    }

    void StartTimer()
    {
        timerText.color = Color.black;
        timeLeft = levelDuration;
        beepTime = defaultBeepTime;
        timerOn = true;
    }
}
