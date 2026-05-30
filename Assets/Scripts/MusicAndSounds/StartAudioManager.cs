using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartAudioManager : MonoBehaviour
{
    public static StartAudioManager Instance;

    [Header("Sources")]
    public AudioSource sfxSource;

    [Header("Ambience Clips")]
    public List<AudioClipData> ambienceClips = new List<AudioClipData>();

    [Header("SFX Clips")]
    public List<AudioClipData> sfxClips = new List<AudioClipData>();

    private Dictionary<string, AudioClip> ambienceDict;
    private Dictionary<string, AudioClip> sfxDict;
    private Dictionary<string, AudioSource> activeAmbienceSources = new Dictionary<string, AudioSource>();

    [System.Serializable]
    public class AudioClipData
    {
        public string id;
        public AudioClip clip;
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

    private void InitializeAudioDictionaries()
    {
        ambienceDict = new Dictionary<string, AudioClip>();
        foreach (var data in ambienceClips)
        {
            if (!string.IsNullOrEmpty(data.id) && data.clip != null)
            {
                ambienceDict[data.id] = data.clip;
            }
        }

        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var data in sfxClips)
        {
            if (!string.IsNullOrEmpty(data.id) && data.clip != null)
            {
                sfxDict[data.id] = data.clip;
            }
        }
    }

    public void PlayAmbience(string id)
    {
        if (ambienceDict.TryGetValue(id, out AudioClip clip))
        {
            // If already playing, don't create duplicate
            if (activeAmbienceSources.ContainsKey(id))
            {
                Debug.LogWarning($"Ambience '{id}' is already playing!");
                return;
            }

            // Create a new AudioSource for this ambience
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clip;
            newSource.loop = true;
            newSource.Play();

            activeAmbienceSources[id] = newSource;
        }
        else
        {
            Debug.LogWarning($"Ambience clip with id '{id}' not found!");
        }
    }

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

    public void StopAllAmbience()
    {
        foreach (var source in activeAmbienceSources.Values)
        {
            source.Stop();
            Destroy(source);
        }
        activeAmbienceSources.Clear();
    }

    public void PlaySFX(string id)
    {
        if (sfxDict.TryGetValue(id, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX clip with id '{id}' not found!");
        }
    }

    public IEnumerator LowerPitch(string id)
    {
        const float decrease = 0.02f;
        var source = activeAmbienceSources[id];
        while (source.pitch > 0)
        {
            source.pitch -= decrease;
            yield return new WaitForSeconds(0.1f);
        }
    }

}