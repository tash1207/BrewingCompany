using UnityEngine;
using UnityEngine.UI;

public class SettingItem : MonoBehaviour
{
    public SettingsManager.SettingItem settingItem;
    [SerializeField] Toggle toggle;

    public void SetToggle(bool value)
    {
        // We don't want the toggle sound to play when initializing the toggle value.
        toggle.SetIsOnWithoutNotify(value);
    }
}
