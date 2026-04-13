using UnityEngine;

public class SlotV2 : MonoBehaviour
{
    [Header("Correct Position")]
    [SerializeField] public int slotID = 1;

    private RectTransform rectTransform;
    private DraggableClueV2 currentClue;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public bool IsOccupied() => currentClue != null;

    public void Occupy(DraggableClueV2 clue)
    {
        currentClue = clue;
    }

    public void Vacate()
    {
        currentClue = null;
    }

    public Vector2 GetSnapPosition() => rectTransform.anchoredPosition;

    public RectTransform GetRect() => rectTransform;
}