using UnityEngine;
using System.Collections.Generic;

public class HighlightActivatorIAVersion : MonoBehaviour
{
    [Header("Input Reference")]
    [SerializeField] private GameInput gameInput; 

    [Header("Settings")]
    public float highlightDuration = 5f;
    public float cooldownDuration = 25f;
    [SerializeField] private float maxDistance = 50f;

    private List<OutlineHighlighter> currentHighlighters = new List<OutlineHighlighter>();

    public float CooldownEndTime { get; private set; }
    public float HighlightEndTime { get; private set; } = -Mathf.Infinity;
    public bool IsHighlighting { get; private set; }

    private void Start()
    {
        if (gameInput != null)
        {
            gameInput.OnHighlightAction += GameInput_OnHighlightAction;
        }
    }

    private void OnDestroy()
    {
        // unsubscribe to prevent errors when changing scenes
        if (gameInput != null)
        {
            gameInput.OnHighlightAction -= GameInput_OnHighlightAction;
        }
    }

    private void GameInput_OnHighlightAction(object sender, System.EventArgs e)
    {
        if (Time.time >= CooldownEndTime && !IsHighlighting)
        {
            HighlightVisibleObjects();
        }
    }

    void Update()
    {
        if (IsHighlighting && Time.time >= HighlightEndTime)
        {
            ClearCurrentHighlights();
        }
    }

    private void HighlightVisibleObjects()
    {
        ClearCurrentHighlights();

        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Clue");
        if (candidates == null || candidates.Length == 0) return;

        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        foreach (GameObject go in candidates)
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

        if (currentHighlighters.Count > 0)
        {
            CooldownEndTime = Time.time + cooldownDuration;
            HighlightEndTime = Time.time + highlightDuration;
            IsHighlighting = true;
        }
    }

    private void ClearCurrentHighlights()
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