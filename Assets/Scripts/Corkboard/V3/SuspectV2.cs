using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SuspectV2 : MonoBehaviour
{
    [Header("Suspect Info")]
    [SerializeField] public int suspectID = 1;

    private RectTransform rectTransform;
    private Image image;
    private Color originalColor;

    private Camera uiCamera;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        if (image != null) originalColor = image.color;

        // Find corkboard camera once
        CorkboardTriggerV2 trigger = Object.FindAnyObjectByType<CorkboardTriggerV2>();
        if (trigger != null && trigger.corkboardCameraGO != null)
        {
            uiCamera = trigger.corkboardCameraGO.GetComponent<Camera>();
        }
        if (uiCamera == null) uiCamera = Camera.main;
    }

    private void Update()
    {
        if (image == null) return;

        bool isHover = RectTransformUtility.RectangleContainsScreenPoint(
            rectTransform,
            Input.mousePosition,
            uiCamera   
        );

        image.color = isHover ? Color.yellow : originalColor;
    }

    public Vector2 GetPosition() => rectTransform.anchoredPosition;
    public RectTransform GetRect() => rectTransform;
    public int SuspectID => suspectID;
}