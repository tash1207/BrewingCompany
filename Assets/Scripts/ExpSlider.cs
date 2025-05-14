using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    [SerializeField] Slider expSlider;
    [SerializeField] TMP_Text currentLevelText;

    void OnEnable()
    {
        Actions.OnExpChanged += UpdateUI;
    }

    void OnDisable()
    {
        Actions.OnExpChanged -= UpdateUI;
    }

    public void UpdateUI(ExpManager expManager)
    {
        expSlider.maxValue = expManager.ExpToNextLevel;
        expSlider.value = expManager.CurrentExp;
        currentLevelText.text = "Level: " + expManager.Level;
    }
}
