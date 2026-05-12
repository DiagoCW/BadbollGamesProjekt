using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles the dragging, dropping, and interaction logic for clue items on the clueboard
/// </summary>
[RequireComponent(typeof(Image))]
public class ClueNode : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Tooltip("The unique identifier corresponding to the inventory item database.")]
    public int itemID;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // State tracking variables
    private Transform previousParent;
    private ClueSlot currentSlot;

    private List<RaycastResult> hitObjects = new System.Collections.Generic.List<RaycastResult>();

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Require a canvas group to manage raycast blocking during drag and drop. If the prefab is missing one, add it to prevent erros
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    /// <summary>
    /// Initializes the clue node with its ID and sprite
    /// </summary>
    public void Setup(int id, Sprite image)
    {
        itemID = id;
        GetComponent<Image>().sprite = image;
    }

    /// <summary>
    /// Triggered when the clue is clicked on and begins moving the mouse
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvas == null) canvas = GetComponentInParent<Canvas>(); // failsafe if the canvas reference was lost

        // If the clue is in a slot, close any thread connections before moving it
        if (currentSlot != null)
        {
            ThreadManager.Instance.RemoveConnection(currentSlot);
            
            // Makes the slot empty
            currentSlot.Vacate();
            currentSlot = null;
        }

        // Store original parent in case the drag is cancelled
        previousParent = transform.parent;

        transform.SetParent(canvas.transform); // Move the item to the root canvas so it is not confind by other clue slot boundaries
        transform.SetAsLastSibling(); // Force the item to render on top of all other UI elements

        canvasGroup.blocksRaycasts = false; // Disable raycasting blocking so the mouse can see through the image to find the clues below it

        // Tell the board we are dragging a photo so the player can't exit
        if (ThreadManager.Instance != null && ThreadManager.Instance.boardTrigger != null)
        {
            ThreadManager.Instance.boardTrigger.isDraggingClue = true;
        }
    }

    /// <summary>
    /// Triggered every frame the mouse moves the clue around
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        // Translates the coordinates of the mouse into local coordinates of the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera, 
            out Vector2 localPointerPosition);

        // Update the physical position of the UI element
        rectTransform.localPosition = localPointerPosition;
    }

    /// <summary>
    /// Triggered when the user releases the mouse button.
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Enable raycasts again so the item can be clicked again
        hitObjects.Clear(); // Clear the previous frame's raycast data
        EventSystem.current.RaycastAll(eventData, hitObjects); // Shoot a raycast through the UI stack at the mouse position

        ClueSlot foundSlot = null;

        // Iterate through UI elements the raycast hit
        foreach (RaycastResult hit in hitObjects)
        {
            ClueSlot slot = hit.gameObject.GetComponent<ClueSlot>(); // Check if the hit element has a clueslot component
            if (slot != null && !slot.IsOccupied) // if valid, stop searching.
            {
                foundSlot = slot;
                break; 
            }
        }

        if (foundSlot != null) // If a valid slot was found
        {
            transform.SetParent(foundSlot.transform, false); // parent the clue to the slot

            // Center its location within the slot
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            // Reset position to 0 so it snaps to the center
            rectTransform.anchoredPosition = Vector3.zero;
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;

            // Update state variables
            currentSlot = foundSlot;
            foundSlot.Occupy(this);
            return;
        }

        // Return to OG parent of no valid target was found
        transform.SetParent(previousParent, false);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;

        ClueSlot oldSlot = previousParent.GetComponent<ClueSlot>();
        if (oldSlot != null) 
        {
            currentSlot = oldSlot;
            oldSlot.Occupy(this);
        }

        // allow the player to exit the board again
        if (ThreadManager.Instance != null && ThreadManager.Instance.boardTrigger != null)
        {
            ThreadManager.Instance.boardTrigger.isDraggingClue = false;
        }
    }

    /// <summary>
    /// Listens for mouse clicks that do not involve dragging.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData) 
    {
        if (eventData.button == PointerEventData.InputButton.Right)  // Right click returns item to inventory
        {
            ReturnToInventory();
        }
        else if (eventData.button == PointerEventData.InputButton.Left) // Left click to connect or disconnect a red thread
        {
            if (currentSlot != null) // only allow connections if the clue is placed in a slot
            {
                ThreadManager.Instance.TryConnect(currentSlot);
            }
        }
    }

    /// <summary>
    /// Removes the clue from the clueboard, and puts it back to the inventory
    /// </summary>
    private void ReturnToInventory() 
    {
        // Clean up board logic
        if (currentSlot != null)
        {
            ThreadManager threadManager = ThreadManager.Instance;
            if (threadManager != null)
            {
                threadManager.RemoveConnection(currentSlot); // Remove visual red threads attached to the slot
            }

            // Mark as empty
            currentSlot.Vacate();
            currentSlot = null;
        }

        // Inform spawner that the item is no longer on the board
        ClueboardSpawner spawner = ClueboardSpawner.Instance;
        if (spawner != null)
        {
            spawner.spawnedItemIDs.Remove(itemID);
        }

        // access the player data
        InventoryObject playerInv = PlayerController.Instance.inventory;

        if (playerInv != null && playerInv.database != null)
        {
            // Retrieve og item data based on ID
            ItemObject itemObj = playerInv.database.GetItem[itemID];

            Item itemToReturn = new Item(itemObj); // construct new runtime item innstance

            itemToReturn.Id = itemID; // Explicitly set ID to prevent it defaulting to 0

            playerInv.AddItem(itemToReturn); // add item to player inventory

            Debug.Log($"Item {itemID} ({itemObj.name}) returned to inventory.");
        }
        else
        {
            Debug.LogWarning("Player inventory reference not found. Item could not be returned.");
        }

        Destroy(gameObject); // Destory UI object 
    }

    /// <summary>
    /// Failsafe executed when the game object or the parent canvas is disabled
    /// </summary>
    private void OnDisable()
    {
        // If the inventory or menu is closed the player is mid drag the event system will fail to call OnEndDrag. Restores the object to a stable state.
        if (canvasGroup != null && !canvasGroup.blocksRaycasts)
        {
            canvasGroup.blocksRaycasts = true; // re enable interactions

            if (previousParent != null) // snap back item to last valid location
            {
                transform.SetParent(previousParent, false);
                rectTransform.localPosition = Vector3.zero;

                ClueSlot oldSlot = previousParent.GetComponent<ClueSlot>();
                if (oldSlot != null) 
                {
                    currentSlot = oldSlot;
                    oldSlot.Occupy(this);
                }
            }
        }

        if (ThreadManager.Instance != null && ThreadManager.Instance.boardTrigger != null)
        {
            ThreadManager.Instance.boardTrigger.isDraggingClue = false;
        }
    }
}