using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip inGameMusic;
    [SerializeField] AudioClip pausedGameMusic;

    private bool shouldPlayMusic;
    private float inGameMusicTimestamp;

    private float inGameMusicVolume = 0.5f;
    private float pausedMusicVolume = 0.33f;

    void Start()
    {
        shouldPlayMusic = SettingsManager.Instance.GetValue(SettingsManager.SettingItem.Music);
        MaybePlayMusic();
    }

    void OnEnable()
    {
        Actions.OnBackgroundMusicToggled += ToggleBackgroundMusic;

        Actions.OnLevelResumed += PlayInGameMusic;
        Actions.OnLevelPaused += PlayPauseMusic;
    }

    void OnDisable()
    {
        Actions.OnBackgroundMusicToggled -= ToggleBackgroundMusic;

        Actions.OnLevelResumed -= PlayInGameMusic;
        Actions.OnLevelPaused -= PlayPauseMusic;
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
        TrackCurrentTimestamp();
        audioSource.clip = inGameMusic;
        audioSource.time = inGameMusicTimestamp;
        audioSource.volume = inGameMusicVolume;
        MaybePlayMusic();
    }

    private void PlayPauseMusic()
    {
        TrackCurrentTimestamp();
        audioSource.clip = pausedGameMusic;
        audioSource.time = inGameMusicTimestamp;
        audioSource.volume = pausedMusicVolume;
        MaybePlayMusic();
    }

    // private void PauseMusic()
    // {
    //     TrackCurrentTimestamp();
    //     PlayPauseMusic();
    // }

    private void TrackCurrentTimestamp()
    {
        audioSource.Pause();
        inGameMusicTimestamp = audioSource.time;
    }
}
