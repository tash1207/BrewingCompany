using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    // TODO: Update this to a map of settings enums to bools.
    private bool mobileSetting = false;
    private bool soundSetting = true;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool GetMobileSetting()
    {
        return mobileSetting;
    }

    public bool GetSoundSetting()
    {
        return soundSetting;
    }

    public void SetMobileSetting(bool value)
    {
        mobileSetting = value;
    }

    public void SetSoundSetting(bool value)
    {
        soundSetting = value;
    }
}
