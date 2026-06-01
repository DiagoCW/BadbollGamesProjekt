using UnityEngine;
using System.Collections;
using System.Threading;

/// <summary>
/// Handles audio crossfading when the player enters or exits a specific area.
/// Requieres a trigger collider on the GameObject this script is attached to.
/// </summary>
public class MusicZone : MonoBehaviour // Author: Stefan Cwiek
{
    public AudioSource mainMusic;
    public AudioSource barMusic;
    public float fadeTime = 2.0f; 
    public float maxVolume = 0.5f; 

    // Tracks the active fade coroutine to interrupt if the player quickly runs in and out
    private Coroutine fadeRoutine;

    private void Awake()
    {
        //PrepareAudioSource(mainMusic);
        PrepareAudioSource(barMusic);
    }

    void Start()
    {
        
    }

    private void PrepareAudioSource(AudioSource src)
    {
        if (src == null) return;

        float originalVol = src.volume;
        // clamp stored volume to the configured maxVolume
        originalVol = Mathf.Clamp(originalVol, 0f, maxVolume);

        // set to silent, play & pause to prime the audio decoding, then restore
        src.volume = 0f;
        src.Play();
        src.Pause();
        src.volume = originalVol;
    }

    /// <summary>
    /// Triggered when another collider enters this objects trigger collider
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If a fade is already happening, stop it to prevent conflicting volume changes
            if (fadeRoutine != null) StopCoroutine(fadeRoutine);

            //Thread musicThread = new Thread(() => { fadeRoutine = StartCoroutine(Crossfade(mainMusic, barMusic)); });
            //musicThread.Start();
            

            // Start fading out the main music for the bar music
            fadeRoutine = StartCoroutine(Crossfade(mainMusic, barMusic));
        }
    }


    /// <summary>
    /// Triggered when another collider leaves this object's trigger collider
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        // React if the object leaving the zone is the player
        if (other.CompareTag("Player"))
        {
            // Stop any ongoing fade
            if (fadeRoutine != null) StopCoroutine(fadeRoutine);

            //Thread musicThread = new Thread(() => { fadeRoutine = StartCoroutine(Crossfade(barMusic, mainMusic)); });
            //musicThread.Start();
            

            // fade out the bar music and fade the main music back in
            fadeRoutine = StartCoroutine(Crossfade(barMusic, mainMusic));
        }
    }

    /// <summary>
    /// Transitions volume between two audio tracks
    /// </summary>
    /// <param name="trackToFadeOut">The audiosource that should go quiet</param>
    /// <param name="trackToFadeIn">The audiosource that should get louder</param>
    /// <returns></returns>
    IEnumerator Crossfade(AudioSource trackToFadeOut, AudioSource trackToFadeIn)
    {
        if (!trackToFadeIn.isPlaying) trackToFadeIn.Play();

        float timer = 0f;

        float startVolOut = trackToFadeOut.volume;
        float startVolIn = trackToFadeIn.volume;

        // if fadus uis barMusic, set ambience to 0
        float targetAmbience = (trackToFadeIn == barMusic) ? 0f : 1f;
        float startAmbience = StartAudioManager.Instance != null ? StartAudioManager.Instance.GetGlobalAmbienceVolume() : 1f; // Get volume the ambience is currently at

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            trackToFadeOut.volume = Mathf.Lerp(startVolOut, 0f, timer / fadeTime);
            trackToFadeIn.volume = Mathf.Lerp(startVolIn, maxVolume, timer / fadeTime);

            if (StartAudioManager.Instance != null) 
            {
                StartAudioManager.Instance.SetGlobalAmbienceVolume(Mathf.Lerp(startAmbience, targetAmbience, timer / fadeTime));
            }

            yield return null;
        }

        // Pause the track that faded out.
        trackToFadeOut.Pause();
    }

}