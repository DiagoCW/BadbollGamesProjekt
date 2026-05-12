using UnityEngine;

/// <summary>
/// Manages player interaction with the physical clueboard object in the game world.
/// Handles camera transitions, cursor state management, and input listening for entering and exiting the board view.
/// </summary>
public class ClueboardTrigger : MonoBehaviour, IInteractable
{
    [Header("Input")]
    [SerializeField] private GameInput gameInput;

    [Header("Cameras")]
    [Tooltip("Player camera")]
    [SerializeField] private GameObject playerCamera;
    [Tooltip("Clueboard camera")]
    [SerializeField] private GameObject clueboardCamera;

    private string playerTag = "Player";

    // State tracking properties
    public bool isViewingBoard { get; private set; } = false;
    public bool isPlayerNear { get; private set; } = false;

    // Flags to prevent the board closing mid interaction
    public bool isDraggingThread = false;
    public bool isDraggingClue = false;

    private bool IsHoldingSomething => isDraggingClue || isDraggingThread;

    private void Start()
    {
        if (gameInput != null)
        {
            gameInput.OnExitAction += GameInput_OnExitAction;
        }

        SetViewToPlayer();
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks when this object is destroyed
        if (gameInput != null)
        {
            gameInput.OnExitAction -= GameInput_OnExitAction;
        }
    }

    /// <summary>
    /// Detects when the player enters the interactable radius of the clueboard.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = true;
        }
    }

    /// <summary>
    /// Detects when the player leaves the interactable radius, closing the board if it is open.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = false;

            // close the board if the player walks away, however not happening when they dragging a thread
            if (isViewingBoard && !IsHoldingSomething)
            {
                ToggleBoard(false);
            }
        }
    }

    public void Interact()
    {
        // Only allow interaction if the player is in range and not currently drawing a connection
        if (isPlayerNear && !IsHoldingSomething)
        {
            ToggleBoard(!isViewingBoard);
        }
    }

    /// <summary>
    /// Exit
    /// </summary>
    private void GameInput_OnExitAction(object sender, System.EventArgs e)
    {
        // Close the board view if it is currently active and no thread is being dragged
        if (isViewingBoard && !isDraggingThread)
        {
            ToggleBoard(false);
        }
    }

    /// <summary>
    /// Handles the transition logic between the player's standard view and the clueboard interface
    /// </summary>
    /// <param name="show">true to enter the board view. false to return to player view</param>
    public void ToggleBoard(bool show)
    {
        isViewingBoard = show;

        if (show)
        {
            // Disable player camera and enable board camera
            playerCamera.SetActive(false);
            clueboardCamera.SetActive(true);

            // Free the cursor and confine it to the game window
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            // Restore player camera and disable board camera
            clueboardCamera.SetActive(false);
            playerCamera.SetActive(true);

            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// resets the view state to the player perspective. used during initialization
    /// </summary>
    private void SetViewToPlayer()
    {
        playerCamera.SetActive(true);
        clueboardCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Failsafe to ensure the cursor remains free and visible while the board is open.
        if (isViewingBoard && !IsHoldingSomething)
        {
            if (Cursor.lockState != CursorLockMode.Confined || !Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}