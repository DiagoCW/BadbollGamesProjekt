using UnityEngine;
using System.Collections;

public class MusicZone : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource barMusic;
    public float fadeTime = 2.0f; // How many seconds the fade takes
    public float maxVolume = 0.5f; // Your preferred music volume

    private Coroutine fadeRoutine;

    void OnTriggerEnter(Collider other)
    {
        // When the player walks IN, fade to the bar music
        if (other.CompareTag("Player"))
        {
            if (fadeRoutine != null) StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(Crossfade(mainMusic, barMusic));
        }
    }

    void OnTriggerExit(Collider other)
    {
        // When the player walks OUT, fade back to the main music
        if (other.CompareTag("Player"))
        {
            if (fadeRoutine != null) StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(Crossfade(barMusic, mainMusic));
        }
    }

    IEnumerator Crossfade(AudioSource trackToFadeOut, AudioSource trackToFadeIn)
    {
        // Make sure the new track is actually running
        if (!trackToFadeIn.isPlaying) trackToFadeIn.Play();

        float timer = 0f;
        float startVolOut = trackToFadeOut.volume;
        float startVolIn = trackToFadeIn.volume;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            // Smoothly slide the volumes up and down
            trackToFadeOut.volume = Mathf.Lerp(startVolOut, 0f, timer / fadeTime);
            trackToFadeIn.volume = Mathf.Lerp(startVolIn, maxVolume, timer / fadeTime);

            yield return null;
        }

        // Pause the old track so it remembers where it left off!
        trackToFadeOut.Pause();
    }
}