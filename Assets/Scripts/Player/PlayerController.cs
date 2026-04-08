using UnityEngine;
using System;

public interface IInteractable
{
    void Interact();
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
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
    [SerializeField] private float interactRange = 2f;

    private CharacterController controller;
    private Vector3 verticalVelocity;
    private float gravity = -9.81f;
    private bool isGrounded;
    
    public bool IsInventoryOpen { get; private set; } = false;
    
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

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("Multiple instances of PlayerController detected! There should only be one PlayerController in the scene.");
        }

        Instance = this;

        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Start()
    {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnInventoryAction += GameInput_OnInventoryAction;
        gameInput.OnInteractAction += GameInput_OnInteractAction;

        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (gameInput != null)
        {
            gameInput.OnJumpAction -= GameInput_OnJumpAction;
            gameInput.OnInventoryAction -= GameInput_OnInventoryAction;
            gameInput.OnInteractAction -= GameInput_OnInteractAction;
        }
    }

    private void GameInput_OnJumpAction(object sender, EventArgs e)
    {
        if (isGrounded && !IsInventoryOpen && !IsDialoguePlaying)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * (gravity * gravityMultiplier));
        }
    }

    private void GameInput_OnInventoryAction(object sender, EventArgs e)
    {
        if (IsDialoguePlaying) 
        {
            return;
        }

        ToggleInventory();
    }

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

            // Check if highlight mode is active
            HighlightActivatorIAVersion highlighter = GameObject
                .FindGameObjectWithTag("Player")
                .GetComponent<HighlightActivatorIAVersion>();

            if (highlighter != null && highlighter.IsHighlighting)
            {
                // Find and call InteractableNPC specifically
                foreach (IInteractable interactable in interactables)
                {
                    if (interactable is InteractableNPC)
                    {
                        interactable.Interact();
                        return;
                    }
                }
            }
            else
            {
                // Find and call DialogueTrigger specifically
                foreach (IInteractable interactable in interactables)
                {
                    if (interactable is DialogueTrigger)
                    {
                        interactable.Interact();
                        return;
                    }
                }
            }
        }

    }

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
            HandleMovement();
    }

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
