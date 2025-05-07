using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] GameObject beerDisplay;
    [SerializeField] TMP_Text beerCount;

    [SerializeField] GameObject poopDisplay;
    [SerializeField] TMP_Text poopCount;

    void OnEnable()
    {
        Actions.OnGlasswareChanged += UpdateGlassware;
        Actions.OnPoopCountChanged += UpdatePoopCount;
    }

    void OnDisable()
    {
        Actions.OnGlasswareChanged -= UpdateGlassware;
        Actions.OnPoopCountChanged -= UpdatePoopCount;
    }

    void UpdateGlassware(int totalGlassware)
    {
        HideDisplays();
        beerDisplay.SetActive(true);
        beerCount.text = totalGlassware.ToString();
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
        poopDisplay.SetActive(false);
    }
}
