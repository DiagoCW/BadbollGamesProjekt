using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Force a UI image to start solid, wait for a duration, and then fade to target transparency
/// </summary>
[RequireComponent(typeof(Image))]
public class CreditsBackgroundFade : MonoBehaviour // Author: Stefan Cwiek
{
    [Header("Fade settings")]
    [Tooltip("How many seconds to wait in pure color before fade starts")]
    [SerializeField] private float delayBeforeFade = 2f;

    [Tooltip("How long is the fade transition")]
    [SerializeField] private float fadeDuration = 4f;

    [Tooltip("The final transparency (0 is invisible, 1 is solid)")]
    [Range(0f, 1f)]
    [SerializeField] private float targetAlpha = 0.7f;

    private Image backgroundImage;

    private void Start()
    {
        backgroundImage = GetComponent<Image>();

        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine() 
    {
        yield return new WaitForSeconds(delayBeforeFade); // wait for the initial delay

        // setup fade variables
        float elapsedTime = 0f;
        Color color = backgroundImage.color;
        float startAlpha = color.a;

        // transition alpha over time
        while (elapsedTime < fadeDuration) 
        {
            elapsedTime += Time.deltaTime;

            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration); // calculates the exact alpha val based on time passed
            backgroundImage.color = color;

            yield return null; // wait for next frame before looping
        }

        // check so it hits the target number at the end
        color.a = targetAlpha;
        backgroundImage.color = color;
    }
}
