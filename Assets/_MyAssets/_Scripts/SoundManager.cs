using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider masterSlider;

    private float sfxVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float masterVolume = 1.0f;
    private float stereoPanning = 0.0f;

    public enum SoundType
    {
        SOUND_SFX,
        SOUND_MUSIC
    }

    // Define the reference value property that will grant access to the class.
    public static SoundManager Instance {  get; private set; }

    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> musicDictionary = new Dictionary<string, AudioClip>();

    private AudioSource sfxSource;
    private AudioSource musicSource;
    

    private void Awake()
    {
        // Instanace creation and enforcement of only one objects.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Initialize();

        }
        else // If instance already exists and points to an instance of SoundManager.
        {
            Debug.Log("Goodbye cruel world!");
            Destroy(gameObject); // Destroy the new instance, so only the original remains.
        }

    }

    // Initialize the SoundManager. 
    private void Initialize()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        sfxSource.volume = sfxVolume * masterVolume;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicSource.volume = musicVolume * masterVolume;
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        sfxSource.volume = sfxVolume * masterVolume;
        musicSource.volume = musicVolume * masterVolume;
    }

    public void SetStereoPanning(float value)
    {
        stereoPanning = value;
        sfxSource.panStereo = stereoPanning;
        musicSource.panStereo = stereoPanning;
    }

    // Add a sound to the dictionary.
    public void AddSound(string soundKey, AudioClip audioClip, SoundType soundType)
    {
        Dictionary<string, AudioClip> targetDictionary = GetDictionaryByType(soundType);

        if (!targetDictionary.ContainsKey(soundKey))
        {
            targetDictionary.Add(soundKey, audioClip);
        }
        else
        {
            Debug.LogWarning("Sound key " + soundKey + " already exists in the " + soundType + " dictionary.");
        }
    }

    // Play a sound by key interface.
    public void PlaySound(string soundKey)
    {
        Play(soundKey, SoundType.SOUND_SFX);
    }

    // Play music by key interface.
    public void PlayMusic(string soundKey)
    {
        musicSource.Stop();
        Play(soundKey, SoundType.SOUND_MUSIC);
    }


    // Play utility.
    private void Play(string soundKey, SoundType soundType)
    {
        Dictionary<string, AudioClip> targetDictionary;
        AudioSource targetSource;

        SetTargetsByType(soundType, out targetDictionary, out targetSource);

        if (targetDictionary.ContainsKey(soundKey))
        {
            targetSource.PlayOneShot(targetDictionary[soundKey]);
        }
        else
        {
            Debug.LogWarning("Sound key " + soundKey + " not found in the " + soundType + " dictionary.");
        }
    }

    private void SetTargetsByType(SoundType soundType, out Dictionary<string, AudioClip> targetDictionary, out AudioSource targetSource)
    {
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                targetDictionary = sfxDictionary;
                targetSource = sfxSource;
                break;
            case SoundType.SOUND_MUSIC:
                targetDictionary = musicDictionary;
                targetSource = musicSource;
                break;
            default:
                Debug.LogError("Unknown sound type: " + soundType);
                targetDictionary = null;
                targetSource = null;
                break;
        }
    }
    private Dictionary<string, AudioClip> GetDictionaryByType(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                return sfxDictionary;
            case SoundType.SOUND_MUSIC:
                return musicDictionary;
            default:
                Debug.LogError("Unknown sound type: " + soundType);
                return null;
        }
    }
}