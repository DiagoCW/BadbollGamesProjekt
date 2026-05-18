using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Handles screen fade ins when a scene starts and fade outs when transitioning to a new scene 
/// </summary>
public class FadeInOut : MonoBehaviour
{
    public static FadeInOut Instance {get; private set; }

    [Header("References")]
    [Tooltip("The black image to fade")]
    [SerializeField] private Image fadeImage;

    [Header("Settings")]
    [Tooltip("Default fade time")]
    public float fadeDuration = 1.5f;

    [Tooltip("Should the screen fade in from black when this scene starts?")]
    public bool FadeInOnStart = true;

    private void Awake()
    {
        if (Instance == null) Instance = this; // Singleton setup
        else Destroy(gameObject);
    }

    void Start()
    {
        if (FadeInOnStart) 
        {
            // Whenever a new scene loads, start fading in from black (alpha 1f) to clear (alpha 0f)
            StartCoroutine(FadeRoutine(1f, 0f, fadeDuration));  // 1f = black, 0f = clear
        }
        else // If a fade in is not desired, instantly make the image invisible and turn off.
        {
            Color clearColor = fadeImage.color;
            clearColor.a = 0f;
            fadeImage.color = clearColor;

            fadeImage.gameObject.SetActive(true); // disable gameobject so it doesnt block anything
        }

    }

    /// <summary>
    /// Fade the screen to black, then load the next scene
    /// </summary>
    /// <param name="sceneName">The exact string of the scene to load</param>
    /// <param name="duration">How long the fade to black should take</param>
    public void FadeToScene(string sceneName, float duration) 
    {
        StartCoroutine(FadeAndLoadRoutine(sceneName, duration));
    }

    /// <summary>
    /// Fades the screen to a specfic alpha without loading a screen. For temporary blackouts. To be improved later
    /// </summary>
    /// <param name="targetAlpha">The target transparency</param>
    /// <param name="duration">for how long</param>
    public void FadeScreenOnly(float targetAlpha, float duration) 
    {
        StartCoroutine(FadeRoutine(fadeImage.color.a, targetAlpha, duration));
    }
    
    /// <summary>
    /// Core method, changes the alpha of the UI image over time.
    /// </summary>
    private IEnumerator FadeRoutine(float startAlpha, float endAlpha, float duration) 
    {
        fadeImage.gameObject.SetActive(true); // Turn on the image
        Color fadeColor = fadeImage.color;
        float timer = 0f;

        // Loop every frame until the timer reaches the destination
        while (timer < duration) 
        {
            timer += Time.deltaTime; // track how much time has passed since last frame
            fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, timer / duration); // Lerp to blend start value and end value based on the percentage of time passed (timer/duration)
            fadeImage.color = fadeColor;
            yield return null; // Pause the loop here, render the frame, and continue to the next frame
        }

        fadeColor.a = endAlpha; // failsafe since lerp isnt always fully precise. Snap to alpha
        fadeImage.color = fadeColor;

        if (endAlpha == 0f) // turn off the image when faded to clear
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// A sequence that waits for the fade to finish before loading the next scene
    /// </summary>
    private IEnumerator FadeAndLoadRoutine(string sceneName, float duration) 
    {
        yield return StartCoroutine(FadeRoutine(0f, 1f, duration)); // Wait for the screen to turn completely black. yield return StartCoroutine will make this coroutine pause intil the FadeRoutine finishes its entire loop.

        SceneManager.LoadScene(sceneName); // When previous step is done, then load the next scene
    }
}

