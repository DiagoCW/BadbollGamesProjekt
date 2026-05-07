using UnityEngine;
using UnityEngine.UI;

public class CooldownUIVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HighlightActivatorIAVersion activator;
    [SerializeField] private Image cooldownFill;     // grey radial – fills 0 to 1 during cooldown
    [SerializeField] private Image activeFill;       // green radial – empties 1 to 0 during active
    [SerializeField] private Image icon;             // magnifying glass icon

    [Header("Colors")]
    [SerializeField] private Color readyColor = Color.white;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 1f); // dim grey

    private void Start()
    {
        ResetVisuals();
    }

    private void Update()
    {
        if (activator == null) return;

        float time = Time.time;

        if (activator.IsHighlighting)
        {
            // Active phase
            float remaining = activator.HighlightEndTime - time;
            float progress = Mathf.Clamp01(remaining / activator.highlightDuration);

            if (activeFill) activeFill.fillAmount = progress;
            if (cooldownFill) cooldownFill.fillAmount = 0f;
            if (icon) icon.color = activeColor;
        }
        else if (time < activator.CooldownEndTime)
        {
            // Cooldown phase
            float remaining = activator.CooldownEndTime - time;
            float progress = Mathf.Clamp01(remaining / activator.cooldownDuration);

            if (cooldownFill) cooldownFill.fillAmount = 1f - progress;
            if (activeFill) activeFill.fillAmount = 0f;
            if (icon) icon.color = cooldownColor;
        }
        else
        {
            ResetVisuals();
        }
    }

    private void ResetVisuals()
    {
        if (cooldownFill) cooldownFill.fillAmount = 0f;
        if (activeFill) activeFill.fillAmount = 0f;
        if (icon) icon.color = readyColor;
    }
}