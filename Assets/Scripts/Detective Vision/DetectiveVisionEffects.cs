using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;  // Required for URP-specific overrides like Vignette

public class DetectiveVisionEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HighlightActivator activator;
    [SerializeField] private Volume volume;  // Drag your Global Volume (DetectiveVisionVolume) here

    [Header("Vision Settings")]
    [SerializeField, Range(-100f, 0f)] private float visionSaturation = -65f;
    [SerializeField, Range(0f, 0.8f)] private float visionVignetteIntensity = 0.4f;  // 0.3–0.55 feels good for tunnel effect
    [SerializeField, Range(0.5f, 15f)] private float lerpSpeed = 8f;  // How fast effects transition

    [Header("Bloom Boost (optional)")]
    [SerializeField] private bool boostBloomDuringHighlight = true;
    [SerializeField, Range(0.5f, 6f)] private float activeBloomIntensity = 3.5f;
    [SerializeField, Range(0.5f, 3f)] private float normalBloomIntensity = 1.0f;

    private VolumeProfile profile;
    private ColorAdjustments colorAdj;
    private Vignette vignette;
    private Bloom bloom;

    private float normalSaturation = 0f;
    private float normalVignette = 0f;

    void Awake()
    {
        if (volume == null)
        {
            Debug.LogWarning("DetectiveVisionEffects: No Volume assigned!", this);
            return;
        }

        profile = volume.profile;
        if (profile == null)
        {
            Debug.LogWarning("DetectiveVisionEffects: Volume has no Profile!", this);
            return;
        }

        // Safely get the overrides (they must exist in your Volume profile!)
        profile.TryGet(out colorAdj);
        profile.TryGet(out vignette);
        profile.TryGet(out bloom);

        if (colorAdj == null) Debug.LogWarning("No Color Adjustments override found in profile.");
        if (vignette == null) Debug.LogWarning("No Vignette override found in profile. Add it via Add Override → Post-processing → Vignette.");
        if (bloom == null && boostBloomDuringHighlight)
            Debug.LogWarning("No Bloom override found, bloom boost disabled.");
    }

    void Update()
    {
        if (activator == null) return;

        float t = Time.deltaTime * lerpSpeed;

        // Saturation (desaturation during highlight)
        if (colorAdj != null)
        {
            float targetSat = activator.IsHighlighting ? visionSaturation : normalSaturation;
            colorAdj.saturation.value = Mathf.Lerp(colorAdj.saturation.value, targetSat, t);
        }

        // Vignette (tunnel vision – stronger when highlighting)
        if (vignette != null)
        {
            float targetVignette = activator.IsHighlighting ? visionVignetteIntensity : normalVignette;
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetVignette, t);
        }

        // Optional Bloom intensity boost during active highlight
        if (boostBloomDuringHighlight && bloom != null)
        {
            float targetBloom = activator.IsHighlighting ? activeBloomIntensity : normalBloomIntensity;
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, targetBloom, t);
        }
    }
}