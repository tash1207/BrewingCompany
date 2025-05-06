using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [SerializeField] AudioSource soundFXObject;

    [SerializeField] AudioClip buttonClickClip;
    [SerializeField] AudioClip buttonToggleClip;

    [SerializeField] AudioClip pickUpClip;
    [SerializeField] AudioClip dropOffClip;

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
        DontDestroyOnLoad(gameObject);
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
        Actions.OnBeerGrabbed += (obj) => {
            PlaySoundFXClip(pickUpClip, 1f);
        };
        Actions.OnGlasswareCleared += (obj) => {
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
        Actions.OnBeerGrabbed -= (obj) => {
            PlaySoundFXClip(pickUpClip, 1f);
        };
        Actions.OnGlasswareCleared -= (obj) => {
            PlaySoundFXClip(dropOffClip, 0.7f);
        };

        Actions.OnButtonClicked -= () => {
            PlaySoundFXClip(buttonClickClip, 1f);
        };
        Actions.OnButtonToggled -= () => {
            PlaySoundFXClip(buttonToggleClip, 1f);
        };
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
        yield return new WaitForSeconds(audioSource.clip.length);
        objectPool.Release(audioSource);
    }
}
