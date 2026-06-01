using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Stefan, Isak, Dennis
/// Displays a counter showing how many seconds detective vision has been actively used
/// </summary>
public class CooldownUIVisual : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the detective vision highlighter")]
    [SerializeField] private HighlightActivatorIAVersion activator;

    [Tooltip("The circle image")]
    [SerializeField] private Image circleImage;

    [Tooltip("The text that displayes secodns")]
    [SerializeField] private TextMeshProUGUI secondsUsedText;

    private void Start()
    {
        SetVisualsActive(false); // make it hidden at game start
    }

    private void Update()
    {
        if (activator == null) return;

        if (PauseMenu.Instance != null && PauseMenu.isPaused) 
        {
            SetVisualsActive(false);
            return;
        }
        

        if (activator.IsHighlighting) // if the player is highlighting
        {
            SetVisualsActive(true); // turn on the UI

            if (secondsUsedText != null) // Update the text counter
            {
                int seconds = Mathf.FloorToInt(activator.TotalTimeUsed); // convert the float into whole numbers
                secondsUsedText.text = seconds.ToString() + "s";
            }
        }
        else
        {
            SetVisualsActive(false); // if DV closed, hide the UI
        }
    }

    /// <summary>
    /// Helper method to turn the image and the timer on/off.
    /// </summary>
    /// <param name="isActive">true to show, false to hide</param>
    private void SetVisualsActive(bool isActive) 
    {
        if (circleImage != null) circleImage.enabled = isActive;
        if (secondsUsedText != null) secondsUsedText.enabled = isActive;
    }
}