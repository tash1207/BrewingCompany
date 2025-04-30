using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public enum SettingItem
    {
        Mobile,
        Music
    }

    public static SettingsManager Instance { get; private set; }

    private Dictionary<SettingItem, bool> settingsValues = new Dictionary<SettingItem, bool>();
    private bool mobileSetting;
    private bool musicSetting;

    private const string mobileSettingName = "MobileSetting";
    private const string musicSettingName = "MusicSetting";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeValues();
    }

    void InitializeValues()
    {
        mobileSetting = PlayerPrefs.GetInt(mobileSettingName, 0) == 1;
        musicSetting = PlayerPrefs.GetInt(musicSettingName, 1) == 1;

        settingsValues.Add(SettingItem.Mobile, mobileSetting);
        settingsValues.Add(SettingItem.Music, musicSetting);
    }

    void SaveCurrentValues()
    {
        PlayerPrefs.SetInt(mobileSettingName, mobileSetting ? 1 : 0);
        PlayerPrefs.SetInt(musicSettingName, musicSetting ? 1 : 0);

        settingsValues[SettingItem.Mobile] = mobileSetting;
        settingsValues[SettingItem.Music] = musicSetting;
    }

    public bool GetSettingValue(SettingItem settingItem)
    {
        return settingsValues[settingItem];
    }

    public bool GetMobileSetting()
    {
        return settingsValues[SettingItem.Mobile];
    }

    public bool GetMusicSetting()
    {
        return settingsValues[SettingItem.Music];
    }

    public void SetMobileSetting(bool value)
    {
        mobileSetting = value;
        SaveCurrentValues();
    }

    public void SetMusicSetting(bool value)
    {
        musicSetting = value;
        SaveCurrentValues();
    }
}
