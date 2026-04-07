using UnityEngine;
using System;

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

    private CharacterController controller;
    private Vector3 verticalVelocity;
    private float gravity = -9.81f;
    private bool isGrounded;
    
    public bool IsInventoryOpen { get; private set; } = false;
    public bool IsInDialogue { get; set; } = false;

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("Multiple instances of PlayerController detected! There should only be one PlayerController in the scene.");
        }

        Instance = this;

        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnInventoryAction += GameInput_OnInventoryAction;

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
        }
    }

    private void GameInput_OnJumpAction(object sender, EventArgs e)
    {
        if (isGrounded && !IsInventoryOpen && !IsInDialogue)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * (gravity * gravityMultiplier));
        }
    }

    private void GameInput_OnInventoryAction(object sender, EventArgs e)
    {
        if (IsInDialogue) 
        {
            return;
        }

        ToggleInventory();
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
        if (!IsInventoryOpen && !IsInDialogue) 
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
