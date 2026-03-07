using UnityEngine;
using System.Collections.Generic;

public class HighlightActivator : MonoBehaviour
{
    [SerializeField] private KeyCode activateKey = KeyCode.V;
    [SerializeField] private string highlightableTag = "Highlightable";
    [SerializeField] public float highlightDuration = 5f;
    [SerializeField] public float cooldownDuration = 25f;
    [SerializeField] private float maxDistance = 50f;

    private List<OutlineHighlighter> currentHighlighters = new List<OutlineHighlighter>();

    public float CooldownEndTime { get; private set; }
    public float HighlightEndTime { get; private set; } = -Mathf.Infinity;
    public bool IsHighlighting { get; private set; }

    void Update()
    {
        // for Safety make sure to clear highlights if duration expires, even if player doesn't press the key again
        if (IsHighlighting && Time.time >= HighlightEndTime)
        {
            ClearCurrentHighlights();
        }

        if (Time.time < CooldownEndTime)
        {
            return;
        }

        if (Input.GetKeyDown(activateKey))
        {
            HighlightVisibleObjects();
        }
    }

    private void HighlightVisibleObjects()
    {
        ClearCurrentHighlights();

        GameObject[] candidates = GameObject.FindGameObjectsWithTag(highlightableTag);
        if (candidates == null || candidates.Length == 0) return;

        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        foreach (GameObject go in candidates) // Only consider objects with a Renderer and that are within the camera's view 
        {
            Renderer rend = go.GetComponent<Renderer>();
            if (rend == null) continue;

            if (!GeometryUtility.TestPlanesAABB(frustumPlanes, rend.bounds)) continue;

            if (Vector3.Distance(Camera.main.transform.position, go.transform.position) > maxDistance)
                continue;

            OutlineHighlighter highlighter = go.GetComponent<OutlineHighlighter>();
            if (highlighter != null)
            {
                highlighter.SetHighlighted(true, highlightDuration);
                currentHighlighters.Add(highlighter);
            }
        }

        if (currentHighlighters.Count > 0) // Only start cooldown if we actually highlighted something
        {
            CooldownEndTime = Time.time + cooldownDuration;
            HighlightEndTime = Time.time + highlightDuration;
            IsHighlighting = true;
        }
    }

    private void ClearCurrentHighlights() // Un-highlight all currently highlighted objects and reset state
    {
        foreach (var h in currentHighlighters)
        {
            if (h != null)
            {
                h.SetHighlighted(false, 0f);
            }
        }

        currentHighlighters.Clear();
        IsHighlighting = false;
        HighlightEndTime = -Mathf.Infinity;
    }
}