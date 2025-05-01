using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] GameObject beerDisplay;
    [SerializeField] TMP_Text beerCount;

    void OnEnable()
    {
        Actions.OnGlasswareChanged += UpdateGlassware;
    }

    void OnDisable()
    {
        Actions.OnGlasswareChanged -= UpdateGlassware;
    }

    void UpdateGlassware(int totalGlassware)
    {
        beerCount.text = totalGlassware.ToString();
    }
}
