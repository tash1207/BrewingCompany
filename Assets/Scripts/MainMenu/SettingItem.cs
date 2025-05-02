using UnityEngine;
using UnityEngine.UI;

public class SettingItem : MonoBehaviour
{
    public SettingsManager.SettingItem settingItem;
    [SerializeField] Toggle toggle;

    public void SetToggle(bool value)
    {
        toggle.isOn = value;
    }
}
