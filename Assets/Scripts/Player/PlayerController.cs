using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// Defines an object that can be interacted with by the player.
/// </summary>
public interface IInteractable
{
    void Interact();
}

/// <summary>
/// The main place for player logic. Handles movement, interaction raycasting,
/// inventory UI state
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // static instance allowing other scripts to easily check player state
    public static PlayerController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameInput gameInput;

    [Header("UI References")]
    [SerializeField] private GameObject inventoryCanvas;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityMultiplier = 2f;

    [Header("Interaction Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] float interactRange = 2f;
    [Header("Interaction UI")]
    [SerializeField] private TextMeshProUGUI interactPromptText;

    // player inventory
    [SerializeField] public InventoryObject inventory;

    private CharacterController controller;
    
    // Physics variables
    private Vector3 verticalVelocity;
    private float gravity = -9.81f;
    private bool isGrounded;
    
    public bool IsInventoryOpen { get; private set; } = false;
    
    // Helper property to check if dialogue is active
    private bool IsDialoguePlaying 
    {
        get 
        {
            if (NewDialogueManager.Instance != null)
            {
                return NewDialogueManager.Instance.dialogueIsPlaying;
            }
            return false;
        }
    }

    public float InteractRange() => interactRange;

    private void Awake()
    {
        // Single player enforcement
        if (Instance != null) 
        {
            Debug.LogError("Multiple instances of PlayerController detected! There should only be one PlayerController in the scene.");
        }

        Instance = this;

        controller = GetComponent<CharacterController>();

        // Fallback in case camera wasn't assigned in the inspector
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Start()
    {
        // Subscribe to input events
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnInventoryAction += GameInput_OnInventoryAction;
        gameInput.OnInteractAction += GameInput_OnInteractAction;

        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(false);
        }

        if (interactPromptText != null)
        {
            interactPromptText.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks when changing scenes
        if (gameInput != null)
        {
            gameInput.OnJumpAction -= GameInput_OnJumpAction;
            gameInput.OnInventoryAction -= GameInput_OnInventoryAction;
            gameInput.OnInteractAction -= GameInput_OnInteractAction;
        }
    }

    private void GameInput_OnJumpAction(object sender, EventArgs e)
    {
        // Only jump if grounded and not in UI menus
        if (isGrounded && !IsInventoryOpen && !IsDialoguePlaying)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * (gravity * gravityMultiplier)); // physics formula for jumping
        }
    }

    private void GameInput_OnInventoryAction(object sender, EventArgs e)
    {
        if (IsDialoguePlaying || inventory == null || inventoryCanvas == null) // Prevent opening inventory during conversation
        {
            return;
        }
        
        ToggleInventory();
    }

    /// <summary>
    /// Fires a raycast from the camera to detect and trigger IInteractable objects.
    /// !!!!!!!!!!!!! REFACTOR ASAP !!!!!!!!!!!!!!
    /// </summary>
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (IsInventoryOpen || IsDialoguePlaying) 
        {
            return;
        }

        if (playerCamera == null) 
        {
            Debug.LogError("PlayerController: No camera assigned for interaction raycasting.");
            return;
        }

        Ray ray = new Ray
        {
            origin = playerCamera.transform.position,
            direction = playerCamera.transform.forward
        };

        // If you want to see the ray when you press interact button you can uncomment this line of code.
        //Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red, 2f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
        {
            IInteractable[] interactables = hitInfo.collider.GetComponents<IInteractable>();
            
            if (interactables.Length == 0) return;

            // Find and call DialogueTrigger specifically
            foreach (IInteractable interactable in interactables)
            {
                //if (interactable is DialogueTrigger || interactable is InteractableItem)
                {
                    interactable.Interact();
                    return;
                }
            }
        }

    }

    /// <summary>
    /// Toggles the inventory interface and updates the cursor lock state and visibility based on the inventory's open
    /// state.
    /// </summary>
    private void ToggleInventory() 
    {
        IsInventoryOpen = !IsInventoryOpen;

        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(IsInventoryOpen);
        }

        if (IsInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
            HandleInteractionPrompt();
            HandleMovement();
    }

    /// <summary>
    /// Check what the player is looking at to display dynamic UI dialogue text.
    /// !!!!!!!!!!!!! REFACTOR ASAP !!!!!!!!!!!!!!
    /// </summary>
    private void HandleInteractionPrompt()
    {
        if (interactPromptText == null || playerCamera == null)
        {
            Debug.LogError("Interact text or player camera is null");
            return;
        }
            
        // Hide prompt if inventory open or dialogue playing
        if (IsInventoryOpen || IsDialoguePlaying)
        {
            if (interactPromptText.gameObject.activeSelf)
                interactPromptText.gameObject.SetActive(false);
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
        {
            // Check if the hit object (or its parents) implement IInteractable
            
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                GameObject target = hitInfo.collider.GetComponentInParent<Transform>().gameObject;
                if (interactable is DialogueTrigger)
                {
                    interactPromptText.text = target.CompareTag("NPC") ? $"Speak to \n{target.name}" : $"Inspect \n{target.name}";
                    if (!interactPromptText.gameObject.activeSelf)
                        interactPromptText.gameObject.SetActive(true);
                    return;
                }
                else if (interactable is InteractableItem)
                {
                    if (target.CompareTag("NPC"))
                    {
                        interactPromptText.text =
                        GetComponent<HighlightActivatorIAVersion>().IsHighlighting ? $"Examine \n{target.name}" : $"Speak to \n{target.name}";
                        if (!interactPromptText.gameObject.activeSelf)
                            interactPromptText.gameObject.SetActive(true);
                        return;
                    }
                    else
                    {
                        interactPromptText.text =
                            GetComponent<HighlightActivatorIAVersion>().IsHighlighting ? $"Examine \n{target.name}" : $"Inspect \n{target.name}";
                        if (!interactPromptText.gameObject.activeSelf)
                            interactPromptText.gameObject.SetActive(true);
                        return;
                    }
                }
                else if (interactable is InteractableTV)
                {
                    interactPromptText.text = $"Turn on \n{target.name}";

                    if (!interactPromptText.gameObject.activeSelf)
                        interactPromptText.gameObject.SetActive(true);

                    return;
                }
            }

            var corkboardTarget =
                hitInfo.collider.GetComponent<CorkboardTriggerV2>();

            //var ctarget = corkboardTarget.GetComponentInParent<Transform>();
            //var trigger = FindAnyObjectByType<CorkboardTriggerV2>();
            if (corkboardTarget != null && !corkboardTarget.isViewingCorkboard && corkboardTarget.isPlayerNearBoard)
            {
                interactPromptText.text = corkboardTarget.isViewingCorkboard ?
                    $"Close \nCorkboard" : "Open \nCorkboard";
                if (!interactPromptText.gameObject.activeSelf)
                    interactPromptText.gameObject.SetActive(true);
                return;
            }

            //var trigger = FindAnyObjectByType<CorkboardTriggerV2>();
            
            //if (corkboardTarget.isViewingCorkboard)
            //{
            //    interactPromptText.gameObject.SetActive(false);
            //}
                
        }

        if (interactPromptText.gameObject.activeSelf)
            interactPromptText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Handles physical movement using Unity's CharacterController
    /// </summary>
    private void HandleMovement() 
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity.y < 0) 
        {
            verticalVelocity.y = -2f; // small negative value to keep the player grounded
        }

        Vector2 inputVector = Vector2.zero;
        if (!IsInventoryOpen && !IsDialoguePlaying) 
        {
            inputVector = gameInput.GetMovementVectorNormalized();
        }

        // Move relative to the player's orientation
        Vector3 moveDir = transform.right * inputVector.x + transform.forward * inputVector.y;
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Apply gravity
        verticalVelocity.y += (gravity * gravityMultiplier) * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
