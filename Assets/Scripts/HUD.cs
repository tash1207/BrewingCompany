using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    [SerializeField] GameObject beerDisplay;
    [SerializeField] TMP_Text beerCount;

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
