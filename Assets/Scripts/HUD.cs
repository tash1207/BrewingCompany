using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] GameObject beerDisplay;
    [SerializeField] TMP_Text beerCount;

    [SerializeField] GameObject busTubDisplay;
    [SerializeField] TMP_Text busTubGlasswareCount;

    [SerializeField] GameObject poopDisplay;
    [SerializeField] TMP_Text poopCount;

    [SerializeField] GameObject expSlider;

    private int currentlyDisplayedAmount;

    void Awake()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.IsStoryMode())
        {
            expSlider.SetActive(true);
        }
    }

    void OnEnable()
    {
        Actions.OnGlasswareChanged += UpdateGlassware;
        Actions.OnBusTubGlasswareCountChanged += UpdateBusTubGlasswareCount;
        Actions.OnPoopCountChanged += UpdatePoopCount;
        Actions.OnMaxGlasswareChanged += MaxGlasswareChanged;
        Actions.ResetLevel += ResetState;
    }

    void OnDisable()
    {
        Actions.OnGlasswareChanged -= UpdateGlassware;
        Actions.OnBusTubGlasswareCountChanged -= UpdateBusTubGlasswareCount;
        Actions.OnPoopCountChanged -= UpdatePoopCount;
        Actions.OnMaxGlasswareChanged -= MaxGlasswareChanged;
        Actions.ResetLevel -= ResetState;
    }

    public void PauseButton()
    {
        Actions.TogglePauseMenu();
    }

    void UpdateGlassware(int totalGlassware)
    {
        HideDisplays();
        beerDisplay.SetActive(true);
        beerCount.text = totalGlassware.ToString() + " / " + SkillsManager.Instance.MaxGlasses;
        currentlyDisplayedAmount = totalGlassware;

        if (totalGlassware > SkillsManager.Instance.MaxGlasses)
        {
            beerCount.color = Color.red;
        }
        else
        {
            beerCount.color = Color.white;
        }
    }

    void UpdateBusTubGlasswareCount(int totalBusTubGlassware)
    {
        HideDisplays();
        busTubDisplay.SetActive(true);
        busTubGlasswareCount.text = totalBusTubGlassware.ToString() + " / 25";
        currentlyDisplayedAmount = totalBusTubGlassware;
    }

    void UpdatePoopCount(int totalPoops)
    {
        HideDisplays();
        poopDisplay.SetActive(true);
        poopCount.text = totalPoops.ToString();
        currentlyDisplayedAmount = totalPoops;
    }

    void MaxGlasswareChanged()
    {
        if (beerDisplay.activeSelf)
        {
            UpdateGlassware(currentlyDisplayedAmount);
        }
    }

    void HideDisplays()
    {
        beerDisplay.SetActive(false);
        busTubDisplay.SetActive(false);
        poopDisplay.SetActive(false);
    }

    private void ResetState()
    {
        UpdateGlassware(0);
    }
}
