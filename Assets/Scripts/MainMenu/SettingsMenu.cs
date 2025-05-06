using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsItems;

    void Start()
    {
        foreach(Transform child in settingsItems.transform)
        {
            if (child.TryGetComponent(out SettingItem settingItem))
            {
                settingItem.SetToggle(
                    SettingsManager.Instance.GetValue(settingItem.settingItem));
            }
        }
    }
}
