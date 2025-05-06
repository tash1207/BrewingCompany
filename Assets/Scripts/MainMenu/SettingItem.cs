using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingItem : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public SettingsManager.SettingItem settingItem;

    [Header("References")]
    [SerializeField] Toggle toggle;
    [SerializeField] Image toggleBackground;

    [Header("Settings")]
    [SerializeField] Color selectedColor;

    public void SetToggle(bool value)
    {
        // We don't want the toggle sound to play when initializing the toggle value.
        toggle.SetIsOnWithoutNotify(value);
    }

    public void OnSelect(BaseEventData eventData)
    {
        toggleBackground.color = selectedColor;
        toggle.graphic.color = selectedColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        toggleBackground.color = Color.white;
        toggle.graphic.color = Color.white;
    }
}
