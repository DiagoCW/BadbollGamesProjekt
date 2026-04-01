using UnityEngine;

public class Slot : MonoBehaviour
{
    [Header("Correct Position")]
    [SerializeField] public int slotID = 1;

    private RectTransform rectTransform;
    private DraggableClue currentClue;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public bool IsOccupied() => currentClue != null;

    public void Occupy(DraggableClue clue)
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