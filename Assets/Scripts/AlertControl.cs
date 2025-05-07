using TMPro;
using UnityEngine;

public class AlertControl : MonoBehaviour
{
    public static AlertControl Instance { get; private set; }

    [SerializeField] GameObject alertDisplay;
    [SerializeField] TMP_Text alertText;

    private bool showingAlert;
    private float alertTimer;
    private float defaultAlertDuration = 2.5f;

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
        Actions.OnLevelEnded += HideAlert;
    }

    void OnDisable()
    {
        Actions.OnLevelEnded -= HideAlert;
    }

    void Update()
    {
        if (showingAlert)
        {
            alertTimer -= Time.deltaTime;

            if (alertTimer <= 0)
            {
                HideAlert();
            }
        }
    }

    public void ShowAlert(string text)
    {
        ShowAlert(text, defaultAlertDuration);
    }

    public void ShowAlert(string text, float duration)
    {
        alertText.text = text;
        alertDisplay.SetActive(true);
        showingAlert = true;
        alertTimer = duration;
    }

    public void HideAlert()
    {
        alertDisplay.SetActive(false);
        showingAlert = false;
        alertTimer = 0f;
    }
}
