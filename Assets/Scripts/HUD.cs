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
        Actions.ResetLevel += ResetState;
    }

    void OnDisable()
    {
        Actions.OnGlasswareChanged -= UpdateGlassware;
        Actions.OnBusTubGlasswareCountChanged -= UpdateBusTubGlasswareCount;
        Actions.OnPoopCountChanged -= UpdatePoopCount;
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
        beerCount.text = totalGlassware.ToString();
    }

    void UpdateBusTubGlasswareCount(int totalBusTubGlassware)
    {
        HideDisplays();
        busTubDisplay.SetActive(true);
        busTubGlasswareCount.text = totalBusTubGlassware.ToString() + " / 25";
    }

    void UpdatePoopCount(int totalPoops)
    {
        HideDisplays();
        poopDisplay.SetActive(true);
        poopCount.text = totalPoops.ToString();
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
