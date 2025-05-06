using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip inGameMusic;

    private bool shouldPlayMusic;

    void Start()
    {
        shouldPlayMusic = SettingsManager.Instance.GetValue(SettingsManager.SettingItem.Music);
        MaybePlayMusic();
    }
    
    void OnEnable()
    {
        Actions.OnBackgroundMusicToggled += ToggleBackgroundMusic;

        Actions.OnLevelStarted += PlayInGameMusic;
        //Actions.OnLevelEnded += StopMusic;
    }

    void OnDisable()
    {
        Actions.OnBackgroundMusicToggled -= ToggleBackgroundMusic;

        Actions.OnLevelStarted -= PlayInGameMusic;
        //Actions.OnLevelEnded -= StopMusic;
    }

    private void MaybePlayMusic()
    {
        if (shouldPlayMusic)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void ToggleBackgroundMusic(bool newValue)
    {
        shouldPlayMusic = newValue;
        MaybePlayMusic();
    }

    private void PlayInGameMusic()
    {
        audioSource.clip = inGameMusic;
        MaybePlayMusic();
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }
}
