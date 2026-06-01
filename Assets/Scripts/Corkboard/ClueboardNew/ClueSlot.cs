using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Author: Stefan Cwiek
/// 
/// Represents an empty clue slot on the board where a clue photo can be placed. 
/// </summary>
public class ClueSlot : MonoBehaviour, IPointerClickHandler
{
    public bool IsOccupied => currentClue != null; // For other scripts to check if a photo is sitting there or not
    public ClueNode currentClue { get; private set; } // The specific photo attached to this slot

    /// <summary>
    /// Links a dropped clue photo to this slot
    /// </summary>
    /// <param name="clue">The clue node being placed here</param>
    public void Occupy(ClueNode clue) 
    {
        currentClue = clue;
    }

    /// <summary>
    /// Clears the slot so a new clue can be placed later.
    /// </summary>
    public void Vacate() 
    {
        currentClue = null;
    }

    /// <summary>
    /// Checks the slot so a new clue can be placed later
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) 
    {
        ThreadManager.Instance.TryConnect(this);
    }
}
