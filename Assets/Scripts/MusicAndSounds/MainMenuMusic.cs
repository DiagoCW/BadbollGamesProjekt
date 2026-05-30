using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Handles the main menu music and a fade out when transitioning to the main game.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MainMenuMusic : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Settings")]
    [Tooltip("How long it takes for the menu music to fade to silence.")]
    [SerializeField] private float fadeDuration = 1.5f;

    [Tooltip("The name of the next scene")]
    [SerializeField] private string sceneToLoad;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGameWithFade()
    {
        // Optional: Disable the button here so the player can't spam click it while it fades
        StartCoroutine(FadeOutAndLoadRoutine());

        if (FadeInOut.Instance != null && !string.IsNullOrEmpty(sceneToLoad)) 
        {
            FadeInOut.Instance.FadeToScene(sceneToLoad, fadeDuration);
        }
        else 
        {
            Debug.LogError("Missing FadeInOut instance or scene name");
        }
    }

    private IEnumerator FadeOutAndLoadRoutine()
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        // Gradually lowers the volume
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null; // Wait for the next frame
        }

        // Turns the volume to 0
        audioSource.volume = 0f;
    }
}