using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Author: Stefan Cwiek
/// 
/// controls the photo of a suspect on the clueboard.
/// starts hidden and becomes visible/clickable when the player unlocks it
/// </summary>
[RequireComponent(typeof(Image))]
public class SuspectNode : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Suspect Info")]
    [Tooltip("Set an ID for the suspect, ex. 0, 1 or 2")]
    public int suspectID;

    public bool isUnlocked { get; private set; } = false;

    private Image photoImage;

    private void Awake()
    {
        photoImage = GetComponent<Image>();

        // hide the photo by making it transparent
        photoImage.color = new Color(1f, 1f, 1f, 0f);

        // disable clicks so the player can't click an invisible suspect
        photoImage.raycastTarget = false;

        // make the photo not stretch or distort
        photoImage.preserveAspect = true;
    }

    /// <summary>
    /// Reveals the suspect on the board and allow the player to interact with it.
    /// </summary>
    /// <param name="photoSprite">The image to display for this suspect.</param>
    public void UnlockSuspect(Sprite photoSprite)
    {
        isUnlocked = true;

        // Show the photo
        photoImage.sprite = photoSprite;
        photoImage.color = new Color(1f, 1f, 1f, 1f);

        // Turn clicks back on
        photoImage.raycastTarget = true;
    }

    /// <summary>
    /// Listens for mouse clicks on the suspects photo.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isUnlocked)
        {
            // Tell the ThreadManager to start pulling a red string from this photo
            ThreadManager.Instance.StartDrawing(this);
        }
    }

    /// <summary>
    /// Triggered when the player clicks and begins moving the mouse
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData) 
    {
        if (isUnlocked && eventData.button == PointerEventData.InputButton.Left)
        {
            ThreadManager.Instance.StartDrawing(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    /// <summary>
    /// Triggered the when the player releases the mouse button after dragging
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isUnlocked || eventData.button != PointerEventData.InputButton.Left) return;

        // Perform a raycast to see what UI element the player dropped the thread onto
        List<RaycastResult> hitObjects = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hitObjects);

        ClueSlot validSlot = null;

        foreach (RaycastResult hit in hitObjects)
        {
            // The player dropped the thread on the Clue slot 
            ClueSlot slot = hit.gameObject.GetComponent<ClueSlot>();
            if (slot != null)
            {
                validSlot = slot;
                break;
            }

            // The player dropped the thread directly on the Clue photo
            ClueNode node = hit.gameObject.GetComponent<ClueNode>();
            if (node != null && node.transform.parent != null)
            {
                ClueSlot parentSlot = node.transform.parent.GetComponent<ClueSlot>();
                if (parentSlot != null)
                {
                    validSlot = parentSlot;
                    break;
                }
            }
        }

        // Connect it if a valid slot with a photo was found under the mouse
        if (validSlot != null && validSlot.IsOccupied)
        {
            ThreadManager.Instance.TryConnect(validSlot);
        }
        else
        {
            // If they missed, or dropped it on an empty slot, cancel the line
            ThreadManager.Instance.CancelDrawing();
        }
    }
}
