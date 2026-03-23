using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameInput gameInput;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityMultiplier = 2f;

    private CharacterController controller;
    private Vector3 verticalVelocity;
    private float gravity = -9.81f;
    private bool isGrounded;

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
    }

    private void GameInput_OnJumpAction(object sender, EventArgs e)
    {
        if (isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * (gravity * gravityMultiplier));
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

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Move relative to the player's orientation
        Vector3 moveDir = transform.right * inputVector.x + transform.forward * inputVector.y;
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Apply gravity
        verticalVelocity.y += (gravity * gravityMultiplier) * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
