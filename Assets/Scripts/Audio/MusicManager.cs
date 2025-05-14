using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip inGameMusic;
    [SerializeField] AudioClip pausedGameMusic;

    private bool shouldPlayMusic;
    private float inGameMusicTimestamp;

    void Start()
    {
        shouldPlayMusic = SettingsManager.Instance.GetValue(SettingsManager.SettingItem.Music);
        MaybePlayMusic();
    }
    
    void OnEnable()
    {
        Actions.OnBackgroundMusicToggled += ToggleBackgroundMusic;

        Actions.OnLevelResumed += PlayInGameMusic;
        Actions.OnLevelPaused += PauseMusic;
    }

    void OnDisable()
    {
        Actions.OnBackgroundMusicToggled -= ToggleBackgroundMusic;

        Actions.OnLevelResumed -= PlayInGameMusic;
        Actions.OnLevelPaused -= PauseMusic;
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
        audioSource.time = inGameMusicTimestamp;
        MaybePlayMusic();
    }

    private void PlayPauseMusic()
    {
        audioSource.clip = pausedGameMusic;
        MaybePlayMusic();
    }

    private void PauseMusic()
    {
        audioSource.Pause();
        inGameMusicTimestamp = audioSource.time;
        PlayPauseMusic();
    }
}
