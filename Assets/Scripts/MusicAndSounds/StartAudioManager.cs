using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Author: Hugo
/// Manages audio playback for the game, including ambience tracks and sound effects.
/// Implements a singleton pattern to persist across scenes. Supports multiple simultaneous
/// ambience tracks and one-shot sound effects. Audio clips are configured via the Inspector
/// using ID-based lookup for scalability.
/// </summary>
public class StartAudioManager : MonoBehaviour
{
    public static StartAudioManager Instance;

    [Header("Sources")]
    public AudioSource sfxSource;

    [Header("Ambience Clips")]
    public List<AudioClipData> ambienceClips = new List<AudioClipData>();

    [Header("SFX Clips")]
    public List<AudioClipData> sfxClips = new List<AudioClipData>();

    private Dictionary<string, AudioClipData> ambienceDict;
    private Dictionary<string, AudioClipData> sfxDict;
    private Dictionary<string, AudioSource> activeAmbienceSources = new Dictionary<string, AudioSource>();

    private float globalAmbienceMultiplier = 1f; // For tracking of global volume so other scripts can quiet it down

    /// <summary>
    /// Data structure for associating an audio clip with a string identifier and volume.
    /// Used in the Inspector to configure available audio clips.
    /// </summary>
    [System.Serializable]
    public class AudioClipData
    {
        public string id;
        public AudioClip clip;
        [Range(0f, 2f)]
        [Tooltip("Volume for this specific audio clip (0 = silent, 1 = full volume, 2 = doubble volume)")]
        public float volume = 1f;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioDictionaries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes dictionaries for fast audio clip lookup by ID.
    /// Called during Awake to prepare audio data from Inspector lists.
    /// </summary>
    private void InitializeAudioDictionaries()
    {
        ambienceDict = new Dictionary<string, AudioClipData>();
        foreach (var data in ambienceClips)
        {
            if (!string.IsNullOrEmpty(data.id) && data.clip != null)
            {
                ambienceDict[data.id] = data;
            }
        }

        sfxDict = new Dictionary<string, AudioClipData>();
        foreach (var data in sfxClips)
        {
            if (!string.IsNullOrEmpty(data.id) && data.clip != null)
            {
                sfxDict[data.id] = data;
            }
        }
    }

    /// <summary>
    /// Plays a looping ambience track by ID. Multiple ambiences can play simultaneously.
    /// Each ambience creates its own AudioSource component. If the same ID is already playing,
    /// a warning is logged and no duplicate is created.
    /// </summary>
    /// <param name="id">The string identifier of the ambience clip to play</param>
    public void PlayAmbience(string id)
    {
        if (ambienceDict.TryGetValue(id, out AudioClipData clipData))
        {
            // If already playing, don't create duplicate
            if (activeAmbienceSources.ContainsKey(id))
            {
                Debug.LogWarning($"Ambience '{id}' is already playing!");
                return;
            }

            // Create a new AudioSource for this ambience
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clipData.clip;
            newSource.volume = clipData.volume;
            newSource.loop = true;
            newSource.Play();

            activeAmbienceSources[id] = newSource;
        }
        else
        {
            Debug.LogWarning($"Ambience clip with id '{id}' not found!");
        }
    }

    /// <summary>
    /// Stops a specific ambience track by ID and destroys its AudioSource component.
    /// </summary>
    /// <param name="id">The string identifier of the ambience clip to stop</param>
    public void StopAmbience(string id)
    {
        if (activeAmbienceSources.TryGetValue(id, out AudioSource source))
        {
            source.Stop();
            Destroy(source);
            activeAmbienceSources.Remove(id);
        }
        else
        {
            Debug.LogWarning($"Ambience '{id}' is not currently playing!");
        }
    }

    /// <summary>
    /// Stops all currently playing ambience tracks and destroys their AudioSource components.
    /// </summary>
    public void StopAllAmbience()
    {
        foreach (var source in activeAmbienceSources.Values)
        {
            source.Stop();
            Destroy(source);
        }
        activeAmbienceSources.Clear();
    }

    /// <summary>
    /// Plays a one-shot sound effect by ID. Multiple sound effects can overlap.
    /// Does not interrupt other playing sounds.
    /// </summary>
    /// <param name="id">The string identifier of the sound effect to play</param>
    public void PlaySFX(string id)
    {
        if (sfxDict.TryGetValue(id, out AudioClipData clipData))
        {
            sfxSource.PlayOneShot(clipData.clip, clipData.volume);
        }
        else
        {
            Debug.LogWarning($"SFX clip with id '{id}' not found!");
        }
    }


    public IEnumerator LowerPitch(string id)
    {
        const float decrease = 0.02f;

        // check if track is playing before trying to get it
        if (!activeAmbienceSources.TryGetValue(id, out var source)) 
        {
            yield break; // exit coroutine
        }

        while (source != null && source.pitch > 0)
        {
            source.pitch -= decrease;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Get volume multiplier
    public float GetGlobalAmbienceVolume() 
    {
        return globalAmbienceMultiplier;
    }

    // update all playing ambience tracks
    public void SetGlobalAmbienceVolume(float multiplier) 
    {
        globalAmbienceMultiplier = multiplier;
        foreach (var kvp in activeAmbienceSources) 
        {
            if (ambienceDict.TryGetValue(kvp.Key, out AudioClipData data)) 
            {
                kvp.Value.volume = data.volume * globalAmbienceMultiplier;
            }
        }
    }

}