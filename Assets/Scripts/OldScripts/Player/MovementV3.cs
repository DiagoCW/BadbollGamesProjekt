using UnityEngine;

public class MovementV3 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float deceleration = 18f;

    [Header("Mouse Look")]
    [SerializeField] private Camera playerCamera;           
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float verticalLookRange = 80f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -30f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 horizontalVelocity;
    private bool isGrounded;
    private float xRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("No CharacterController found on " + gameObject.name);
            enabled = false;
            return;
        }

        // Auto-create ground check
        if (groundCheck == null)
        {
            GameObject go = new GameObject("GroundCheck");
            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector3(0, 0.1f, 0);
            groundCheck = go.transform;
        }

        // Auto-find camera if not assigned
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera == null)
            {
                Debug.LogWarning("No Camera found as child of player. Please assign it in Inspector.", this);
            }
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!enabled) return;

        HandleGroundCheck();
        HandleMovement();
        HandleJump();

        // Only run mouse look if we have a valid camera
        if (playerCamera != null)
        {
            HandleMouseLook();
        }

        controller.Move(horizontalVelocity * Time.deltaTime + velocity * Time.deltaTime);
    }

    private void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = transform.right * x + transform.forward * z;
        inputDir = Vector3.ClampMagnitude(inputDir, 1f);

        float currentMaxSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 targetVelocity = inputDir * currentMaxSpeed;

        float accel = (targetVelocity.magnitude > 0.01f) ? acceleration : deceleration;
        horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, targetVelocity, accel * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
    }

    private void HandleMouseLook()
    {
        if (playerCamera == null) return;   // Safety check

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookRange, verticalLookRange);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}