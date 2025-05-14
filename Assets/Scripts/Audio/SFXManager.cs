using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [SerializeField] AudioSource soundFXObject;

    [Header("UI Sounds")]
    [SerializeField] AudioClip buttonClickClip;
    [SerializeField] AudioClip buttonToggleClip;

    [Header("Inventory Sounds")]
    [SerializeField] AudioClip pickUpClip;
    [SerializeField] AudioClip dropOffClip;
    [SerializeField] AudioClip glassBreakingClip;

    [Header("Dog Sounds")]
    [SerializeField] AudioClip[] dogBarkClips;
    [SerializeField] AudioClip dogBreathingClip;

    [Header("Game Sounds")]
    [SerializeField] AudioClip timerBeepClip;

    private ObjectPool<AudioSource> objectPool;
    private int poolDefaultCapacity = 4;
    private int poolMaxSize = 10;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void OnEnable()
    {
        SetUpObjectPool();
        SetUpEventListeners();
    }

    void OnDisable()
    {
        RemoveEventListeners();
    }

    void SetUpObjectPool()
    {
        objectPool = new ObjectPool<AudioSource>(() => {
            return Instantiate(soundFXObject, transform);
        }, obj => {
            obj.gameObject.SetActive(true);
        }, obj => {
            obj.gameObject.SetActive(false);
        }, obj => {
            Destroy(obj);
        }, /* collectionCheck= */ true, poolDefaultCapacity, poolMaxSize);
    }

    void SetUpEventListeners()
    {
        Actions.OnItemPickedUp += (obj) => {
            PlaySoundFXClip(pickUpClip, 1f);
        };
        Actions.OnGlasswareCleared += (obj) => {
            PlaySoundFXClip(dropOffClip, 0.7f);
        };
        Actions.OnPoopsThrownAway += (obj) => {
            PlaySoundFXClip(dropOffClip, 0.7f);
        };

        Actions.OnButtonClicked += () => {
            PlaySoundFXClip(buttonClickClip, 1f);
        };
        Actions.OnButtonToggled += () => {
            PlaySoundFXClip(buttonToggleClip, 1f);
        };
    }

    void RemoveEventListeners()
    {
        Actions.OnItemPickedUp -= (obj) => {
            PlaySoundFXClip(pickUpClip, 1f);
        };
        Actions.OnGlasswareCleared -= (obj) => {
            PlaySoundFXClip(dropOffClip, 0.7f);
        };
        Actions.OnPoopsThrownAway -= (obj) => {
            PlaySoundFXClip(dropOffClip, 0.7f);
        };

        Actions.OnButtonClicked -= () => {
            PlaySoundFXClip(buttonClickClip, 1f);
        };
        Actions.OnButtonToggled -= () => {
            PlaySoundFXClip(buttonToggleClip, 1f);
        };
    }

    public void PlayDogBark()
    {
        AudioClip dogBarkClip = dogBarkClips[Random.Range(0, dogBarkClips.Length)];
        PlaySoundFXClip(dogBarkClip, 1f);
    }

    public void PlayDogBreathing()
    {
        PlaySoundFXClip(dogBreathingClip, 0.65f);
    }

    public void PlayGlassBreaking()
    {
        PlaySoundFXClip(glassBreakingClip, 1f);
    }

    public void PlayTimerBeep()
    {
        PlaySoundFXClip(timerBeepClip, 0.9f);
    }

    public void PlaySoundFXClip(AudioClip audioClip, float volume)
    {
        AudioSource audioSource = objectPool.Get();
        audioSource.transform.position = Camera.main.transform.position;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        StartCoroutine(ReturnAudioSourceToPool(audioSource));
    }

    IEnumerator ReturnAudioSourceToPool(AudioSource audioSource)
    {
        yield return new WaitForSecondsRealtime(audioSource.clip.length);
        objectPool.Release(audioSource);
    }
}
