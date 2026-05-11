using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;  

public class DetectiveVisionEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HighlightActivatorIAVersion activator;
    [SerializeField] private Volume volume;  

    [Header("Vision Settings")]
    [SerializeField, Range(-100f, 0f)] private float visionSaturation = -65f;
    [SerializeField, Range(0f, 0.8f)] private float visionVignetteIntensity = 0.8f;  
    [SerializeField, Range(0.5f, 15f)] private float lerpSpeed = 8f;  

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

    void Start()
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

        if (colorAdj != null)
        {
            if (PlayerController.Instance.IsInventoryOpen || NewDialogueManager.Instance.dialogueIsPlaying)
                colorAdj.saturation.value = Mathf.Lerp(colorAdj.saturation.value, normalSaturation, t);
            else
            {
                float targetSat = activator.IsHighlighting ? visionSaturation : normalSaturation;
                colorAdj.saturation.value = Mathf.Lerp(colorAdj.saturation.value, targetSat, t);
            }
        }

        
        if (vignette != null)
        {
            if (PlayerController.Instance.IsInventoryOpen || NewDialogueManager.Instance.dialogueIsPlaying)
                vignette.intensity.value = normalVignette = Mathf.Lerp(vignette.intensity.value, normalVignette, t);
            else
            {
                float targetVignette = activator.IsHighlighting ? visionVignetteIntensity : normalVignette;
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetVignette, t);
            }
        }

        
        if (boostBloomDuringHighlight && bloom != null)
        {
            if (PlayerController.Instance.IsInventoryOpen || NewDialogueManager.Instance.dialogueIsPlaying)
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, normalBloomIntensity, t);
            else
            {
                float targetBloom = activator.IsHighlighting ? activeBloomIntensity : normalBloomIntensity;
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, targetBloom, t);
            }
        }
    }
}