using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// controls the photo of a suspect on the clueboard.
/// starts hidden and becomes visible/clickable when the player unlocks it
/// </summary>
[RequireComponent(typeof(Image))]
public class SuspectNode : MonoBehaviour, IPointerClickHandler
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
}