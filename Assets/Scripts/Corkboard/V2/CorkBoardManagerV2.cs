using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CorkboardManagerV2 : MonoBehaviour
{
    public static event System.Action OnCorkboardCompleted;

    [Header("UI References")]
    [SerializeField] private Button finishButton;
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private TextMeshProUGUI resultText;

    [Header("End Scene Camera")]
    [SerializeField] private Camera endSceneCamera;

    [Header("Wire System")]
    [SerializeField] private ConnectionLineV2 linePrefab;
    [SerializeField] private RectTransform wiresContainer;
    [SerializeField] private RectTransform corkboardCanvasRect;
    [SerializeField] private float connectDistance = 120f;
    [SerializeField] private float minDragDistance = 15f;   // ← Prevents click = connect

    private bool isPuzzleSolved = false;
    private SlotV2[] clueSlots;
    private SuspectV2[] suspects;

    private List<ConnectionData> connections = new List<ConnectionData>();
    private SuspectV2 currentDraggingSuspect = null;
    private ConnectionLineV2 tempLine = null;
    private Vector2 dragStartPosition;

    private Camera uiCamera;

    [System.Serializable]
    private struct ConnectionData
    {
        public SuspectV2 suspect;
        public DraggableClueV2 clue;
        public ConnectionLineV2 line;
    }

    private void Awake()
    {
        clueSlots = Object.FindObjectsByType<SlotV2>(FindObjectsSortMode.None);
        suspects = Object.FindObjectsByType<SuspectV2>(FindObjectsSortMode.None);

        CorkboardTrigger trigger = Object.FindAnyObjectByType<CorkboardTrigger>();
        if (trigger?.corkboardCameraGO != null)
            uiCamera = trigger.corkboardCameraGO.GetComponent<Camera>();
        if (uiCamera == null) uiCamera = Camera.main;
    }

    private void Update()
    {
        if (isPuzzleSolved) return;
        HandleWireDragging();

        if (AnySuspectHasThreeOrMoreConnections() && finishButton != null && !finishButton.gameObject.activeSelf)
            finishButton.gameObject.SetActive(true);
    }

    private bool AnySuspectHasThreeOrMoreConnections()
    {
        foreach (var s in suspects)
            if (connections.Count(c => c.suspect == s) >= 3) return true;
        return false;
    }

    private void HandleWireDragging()
    {
        if (uiCamera == null) return;

        // Drag start
        if (currentDraggingSuspect == null && Input.GetMouseButtonDown(0))
        {
            foreach (var suspect in suspects)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(suspect.GetRect(), Input.mousePosition, uiCamera))
                {
                    currentDraggingSuspect = suspect;
                    dragStartPosition = Input.mousePosition;
                    if (linePrefab && wiresContainer)
                        tempLine = Instantiate(linePrefab.gameObject, wiresContainer).GetComponent<ConnectionLineV2>();
                    return;
                }
            }
        }

        // Update Line
        if (currentDraggingSuspect != null && tempLine != null && Input.GetMouseButton(0))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(corkboardCanvasRect, Input.mousePosition, uiCamera, out Vector2 localMouse))
                tempLine.UpdateTempLine(currentDraggingSuspect.GetPosition(), localMouse);
        }

        //on Realese
        if (currentDraggingSuspect != null && Input.GetMouseButtonUp(0))
        {
            // Only connect if player actually dragged a bit
            if (Vector2.Distance(dragStartPosition, Input.mousePosition) > minDragDistance)
            {
                TryCreatePermanentConnection();
            }

            if (tempLine != null)
            {
                Destroy(tempLine.gameObject);
                tempLine = null;
            }
            currentDraggingSuspect = null;
        }
    }

    private void TryCreatePermanentConnection()
    {
        if (currentDraggingSuspect == null) return;

        SlotV2 bestSlot = null;
        float bestDist = float.MaxValue;

        foreach (var slot in clueSlots)
        {
            DraggableClueV2 clue = slot.GetComponentInChildren<DraggableClueV2>();
            if (clue == null) continue;

            //One clue can only belong to ONE suspect
            if (connections.Exists(c => c.clue == clue))
                continue;

            // Max 3 per suspect
            if (connections.Count(c => c.suspect == currentDraggingSuspect) >= 3)
                continue;

            float dist = Vector2.Distance(currentDraggingSuspect.GetRect().position, slot.GetRect().position);

            if (dist < bestDist)
            {
                bestDist = dist;
                bestSlot = slot;
            }
        }

        if (bestSlot != null && bestDist <= connectDistance)
        {
            DraggableClueV2 clue = bestSlot.GetComponentInChildren<DraggableClueV2>();

            GameObject lineGO = Instantiate(linePrefab.gameObject, wiresContainer);
            ConnectionLineV2 newLine = lineGO.GetComponent<ConnectionLineV2>();
            newLine.SetPermanentPoints(currentDraggingSuspect.GetRect(), bestSlot.GetRect());

            connections.Add(new ConnectionData { suspect = currentDraggingSuspect, clue = clue, line = newLine });
        }
    }

    // Called from DraggableClueV2 when clue is moved
    public void RemoveConnectionsForClue(DraggableClueV2 clue)
    {
        for (int i = connections.Count - 1; i >= 0; i--)
        {
            if (connections[i].clue == clue)
            {
                if (connections[i].line != null)
                    Destroy(connections[i].line.gameObject);
                connections.RemoveAt(i);
            }
        }
    }

    public void OnFinishButtonPressed()
    {
        if (finishButton != null)
        {
            finishButton.gameObject.SetActive(false);
            finishButton.interactable = false;
        }
        CalculateAndShowResult();
    }

    private void CalculateAndShowResult()
    {
        bool won = AnySuspectHasThreeOrMoreConnections();
        if (won) OnCorkboardCompleted?.Invoke();

        CorkboardTrigger trigger = Object.FindAnyObjectByType<CorkboardTrigger>();
        if (trigger != null) trigger.ForceExit();

        if (endSceneCamera != null)
        {
            Camera.main.gameObject.SetActive(false);
            endSceneCamera.gameObject.SetActive(true);
        }

        if (resultCanvas != null)
        {
            resultCanvas.SetActive(true);
            if (resultText != null)
            {
                int max = 0;
                foreach (var s in suspects) max = Mathf.Max(max, connections.Count(c => c.suspect == s));
                resultText.text = won ? $"You Win!\n\n{max} clues connected" : "Game Over";
            }
        }
    }
}