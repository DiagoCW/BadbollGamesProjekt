using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class HighlightActivatorIAVersion : MonoBehaviour
{
    [Header("Input Reference")]
    [SerializeField] private GameInput gameInput;
    /*[SerializeField]*/ private Camera playerCamera;
    float currentFOV;
    float zoomFOV;

    [Header("Settings")]
    public float highlightDuration = 5f;
    public float cooldownDuration = 25f;
    public float totalTimeUsed = 0f;
    [SerializeField] private float maxDistance = 15f;

    private List<OutlineHighlighter> currentHighlighters = new List<OutlineHighlighter>();

    public float CooldownEndTime { get; private set; }
    public float HighlightEndTime { get; private set; } = -Mathf.Infinity;
    public bool IsHighlighting { get; private set; }

    private void Start()
    {
        if (gameInput != null)
        {
            gameInput.OnHighlightAction += GameInput_OnHighlightAction;
            gameInput.OnHighlightCancel += GameInput_OnHighlightCancel;
        }
        playerCamera = GetComponentInChildren<Camera>();
        currentFOV = playerCamera.fieldOfView;
        zoomFOV = playerCamera.fieldOfView - 30;
    }

    private void OnDestroy()
    {
        // unsubscribe to prevent errors when changing scenes
        if (gameInput != null)
        {
            gameInput.OnHighlightAction -= GameInput_OnHighlightAction;
            gameInput.OnHighlightCancel -= GameInput_OnHighlightCancel;
        }
    }

    private void GameInput_OnHighlightAction(object sender, System.EventArgs e)
    {
        if (playerCamera == null || !playerCamera.isActiveAndEnabled) return; // Prevent use if the camera is disabled

        if (ThreadManager.Instance != null && ThreadManager.Instance.boardTrigger != null && ThreadManager.Instance.boardTrigger.isViewingBoard) return; // Prevent use if player is looking at clueboard

      //  if (PlayerController.Instance.IsInventoryOpen || NewDialogueManager.Instance.dialogueIsPlaying) return; // Prevent use if inventory or dialogue is open

        HighlightVisibleObjects();
        
    }

    private void GameInput_OnHighlightCancel(object sender, System.EventArgs e)
    {
        ClearCurrentHighlights();
    }

    void Update()
    {
        if (IsHighlighting)
        {
            RefreshHighlights();
            totalTimeUsed += Time.deltaTime;
        }
        
        if (PlayerController.Instance.IsInventoryOpen || NewDialogueManager.Instance.dialogueIsPlaying)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, currentFOV, 10f * Time.deltaTime);
            ClearCurrentHighlights();
        }
        //else
        {
            float desiredFOV = IsHighlighting ? zoomFOV : currentFOV;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, desiredFOV, 8f * Time.deltaTime);
        }
        //Debug.Log($"Detective vision used for {totalTimeUsed} seconds");
    }

    /// <summary>
    /// Add any newly visible candidates to the currentHighlighters list and remove
    /// ones that are no longer visible or are out of range. This is called every
    /// frame while the highlight input is held so highlights update dynamically.
    /// </summary>
    private void RefreshHighlights()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Clue");
        if (candidates == null || candidates.Length == 0)
        {
            // If there are no candidates in the scene, clear everything
            ClearCurrentHighlights();
            return;
        }

        if (playerCamera == null || !playerCamera.isActiveAndEnabled) return;

        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(playerCamera);

        var desired = new System.Collections.Generic.HashSet<OutlineHighlighter>();

        foreach (GameObject go in candidates)
        {
            Renderer rend = go.GetComponent<Renderer>();
            if (rend == null) continue;

            if (!GeometryUtility.TestPlanesAABB(frustumPlanes, rend.bounds)) continue;

            if (Vector3.Distance(playerCamera.transform.position, go.transform.position) > maxDistance)
                continue;

            OutlineHighlighter highlighter = go.GetComponent<OutlineHighlighter>();
            if (highlighter != null)
            {
                desired.Add(highlighter);
            }
        }

        // Add new highlights
        foreach (var h in desired)
        {
            if (!currentHighlighters.Contains(h))
            {
                h.SetHighlighted(true, highlightDuration);
                currentHighlighters.Add(h);
            }
        }

        // Remove highlights that are no longer desired
        for (int i = currentHighlighters.Count - 1; i >= 0; i--)
        {
            var h = currentHighlighters[i];
            if (h == null || !desired.Contains(h))
            {
                if (h != null) h.SetHighlighted(false, 0f);
                currentHighlighters.RemoveAt(i);
            }
        }
    }

    private void HighlightVisibleObjects()
    {
        //ClearCurrentHighlights();

        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Clue");
        if (candidates == null || candidates.Length == 0) return;

        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(playerCamera);

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
                highlighter.SetHighlighted(true, totalTimeUsed);
                currentHighlighters.Add(highlighter);
                
            }
        }

        //if (currentHighlighters.Count > 0)
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