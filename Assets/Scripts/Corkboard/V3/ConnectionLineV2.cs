using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class ConnectionLineV2 : MonoBehaviour
{
    [SerializeField] private float lineThickness = 5f;
    [SerializeField] private Color lineColor = new Color(1f, 0.65f, 0f, 0.95f); // orange wire

    private RectTransform rectTransform;
    private Image image;

    // For permanent lines
    private RectTransform pointA; // suspect
    private RectTransform pointB; // clue slot

    // For temporary dragging
    private Vector2 tempStartPos;
    private Vector2 tempEndPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        if (image != null) image.color = lineColor;

        //Pivot at left center
        rectTransform.pivot = new Vector2(0f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
    }

    public void SetPermanentPoints(RectTransform startSuspect, RectTransform endClueSlot)
    {
        pointA = startSuspect;
        pointB = endClueSlot;
        UpdateLine();
    }

    public void UpdateTempLine(Vector2 startSuspectPos, Vector2 mouseLocalPos)
    {
        tempStartPos = startSuspectPos;
        tempEndPos = mouseLocalPos;
        UpdateLine();
    }

    public void UpdateLine()
    {
        if (rectTransform == null) return;

        Vector2 start = pointA != null ? pointA.anchoredPosition : tempStartPos;
        Vector2 end = pointB != null ? pointB.anchoredPosition : tempEndPos;

        Vector2 dir = end - start;
        float length = dir.magnitude;

        if (length < 1f)
        {
            gameObject.SetActive(false);
            return;
        }

        // Position at start point
        rectTransform.anchoredPosition = start;

        // Set length
        rectTransform.sizeDelta = new Vector2(length, lineThickness);

        // Rotate correctly
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rectTransform.localRotation = Quaternion.Euler(0, 0, angle);

        gameObject.SetActive(true);
    }

    public void DestroyLine() => Destroy(gameObject);
}