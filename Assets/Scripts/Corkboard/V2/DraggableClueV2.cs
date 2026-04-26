using UnityEngine;
using UnityEngine.UI;

public class DraggableClueV2 : MonoBehaviour
{
    [SerializeField] private float grabProximityPx = 60f;
    [SerializeField] private float snapDistance = 100f;

    [Header("Correct Position")]
    [SerializeField] public int correctSlotID = 1;   // Kept for future use if needed

    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private Image image;
    private Color originalColor;
    private Vector3 originalScale;

    private Transform originalParent;
    private Vector2 dragOffset;

    private bool isDragging = false;
    private static bool isAnythingBeingDragged = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas?.GetComponent<RectTransform>();

        if (image != null)
        {
            originalColor = image.color;
            originalScale = transform.localScale;
        }

        if (!canvas || !canvasRectTransform)
        {
            enabled = false;
        }
    }

    private void Start()
    {
        originalParent = transform.parent;
    }

    private void Update()
    {
        if (!enabled) return;

        Camera activeCam = Camera.current ?? Camera.main;
        if (activeCam == null) return;

        Vector2 mouse = Input.mousePosition;

        Vector3 screenCenter3D = activeCam.WorldToScreenPoint(rectTransform.position);
        if (screenCenter3D.z < 0) return;

        Vector2 screenCenter = new Vector2(screenCenter3D.x, screenCenter3D.y);
        float dist = Vector2.Distance(mouse, screenCenter);

        if (Input.GetMouseButtonDown(0) && !isDragging && !isAnythingBeingDragged)
        {
            if (dist <= grabProximityPx)
            {
                isDragging = true;
                isAnythingBeingDragged = true;

                // Remove any existing wires before dragging
                CorkboardManagerV2 manager = Object.FindAnyObjectByType<CorkboardManagerV2>();
                if (manager != null) manager.RemoveConnectionsForClue(this);

                SlotV2 currentSlot = GetComponentInParent<SlotV2>();
                if (currentSlot != null)
                {
                    currentSlot.Vacate();
                }

                transform.SetParent(canvas.transform);
                transform.SetAsLastSibling();

                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mouse, activeCam, out Vector2 localMousePos);
                dragOffset = rectTransform.anchoredPosition - localMousePos;

                if (image != null)
                {
                    image.color = new Color(1f, 0.9f, 0.6f);
                    transform.localScale = originalScale * 1.08f;
                }
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mouse, activeCam, out Vector2 localPos))
                {
                    rectTransform.anchoredPosition = localPos + dragOffset;
                }
            }
            else
            {
                isDragging = false;
                isAnythingBeingDragged = false;

                if (image != null)
                {
                    image.color = originalColor;
                    transform.localScale = originalScale;
                }

                SlotV2 nearest = FindNearestSlot();

                if (nearest != null && nearest.GetComponentInChildren<DraggableClueV2>() == null)
                {
                    // Snap to new slot
                    transform.SetParent(nearest.transform, true);

                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.localRotation = Quaternion.identity;

                    nearest.Occupy(this);
                }
                else
                {
                    // Return to tray
                    transform.SetParent(originalParent, true);
                }
            }
        }
    }

    private SlotV2 FindNearestSlot()
    {
        SlotV2[] slots = Object.FindObjectsByType<SlotV2>(FindObjectsSortMode.None);
        SlotV2 closest = null;
        float minDist = snapDistance;

        foreach (SlotV2 s in slots)
        {
            float dist = Vector2.Distance(rectTransform.position, s.GetRect().position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = s;
            }
        }
        return closest;
    }
}