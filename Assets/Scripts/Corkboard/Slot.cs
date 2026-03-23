using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private RectTransform rectTransform;
    private DraggableClue currentClue;   // null if empty

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

    public Vector2 GetSnapPosition()
    {
        return rectTransform.anchoredPosition;   // Center of slot
    }

    public RectTransform GetRect() => rectTransform;

}