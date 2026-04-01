using UnityEngine;
using UnityEngine.UI;

public class DraggableClue : MonoBehaviour
{
    [SerializeField] private float grabProximityPx = 60f;
    [SerializeField] private float snapDistance = 100f;

    [Header("Correct Position")]
    [SerializeField] public int correctSlotID = 1;

    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private Image image;
    private Color originalColor;
    private Vector3 originalScale;

    private Vector2 startPosition;
    private Transform originalParent;

    private bool isDragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas?.GetComponent<RectTransform>();

        originalParent = transform.parent;
        startPosition = rectTransform.anchoredPosition;

        if (image != null)
        {
            originalColor = image.color;
            originalScale = transform.localScale;
        }

        if (!canvas || !canvasRectTransform)
        {
            enabled = false;
            return;
        }

        rectTransform.pivot = new Vector2(0.5f, 0.5f);
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

        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            if (dist <= grabProximityPx)
            {
                isDragging = true;

                Slot currentSlot = GetComponentInParent<Slot>();
                if (currentSlot != null)
                {
                    currentSlot.Vacate();
                }

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
                Vector2 localPos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRectTransform,
                    mouse,
                    activeCam,
                    out localPos))
                {
                    rectTransform.anchoredPosition = localPos;
                }
            }
            else
            {
                isDragging = false;

                if (image != null)
                {
                    image.color = originalColor;
                    transform.localScale = originalScale;
                }

                Slot nearest = FindNearestSlot();
                if (nearest != null && !nearest.IsOccupied())
                {
                    transform.SetParent(nearest.transform);
                    rectTransform.anchoredPosition = Vector2.zero;
                    nearest.Occupy(this);
                }
                else
                {
                    transform.SetParent(originalParent);
                    rectTransform.anchoredPosition = startPosition;
                }
            }
        }
    }

    private Slot FindNearestSlot()
    {
        Slot[] slots = Object.FindObjectsByType<Slot>(FindObjectsSortMode.None);
        Slot closest = null;
        float minDist = snapDistance;

        foreach (Slot s in slots)
        {
            float dist = Vector2.Distance(rectTransform.anchoredPosition, s.GetRect().anchoredPosition);
            if (dist < minDist)
            {
                minDist = dist;
                closest = s;
            }
        }
        return closest;
    }
}