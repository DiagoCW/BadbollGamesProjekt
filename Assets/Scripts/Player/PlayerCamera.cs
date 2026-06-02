using UnityEngine;

/// <summary>
/// Controls the player camera movement. Handles vertical pitch on the camera and horizontal yaw on the player's body
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform playerBody;

    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 15f;
    [SerializeField] private float minPitch = -80f; // max looking down angle
    [SerializeField] private float maxPitch = 80f; // max looking up angle

    [Header("Cinematic Settings")]
    [Tooltip("How much higher than the target's origin the camera should look)")]
    [SerializeField] private float cinematicHeightOffset = 1.5f;

    private float xRotation = 0f; // Tracks up/down rotation state to allow for clamping.
   // private float yRotation = 0f; // Tracks left/right rotation state

    private Transform cinematicTarget;
    private bool isCinematicLook = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetCinematicTarget(Transform target)
    {
        cinematicTarget = target;
        isCinematicLook = (target != null);
    }

    private void LateUpdate()
    {
        // Prevent camera movement if the inventory is open so the player can use their mouse
        if (PlayerController.Instance != null && PlayerController.Instance.IsInventoryOpen)
        {
            return;
        }

        if (isCinematicLook && cinematicTarget != null)
        {
            Vector3 targetPosition = cinematicTarget.position + (Vector3.up * cinematicHeightOffset);

            // Calculate the direction to the adjusted target position
            Vector3 direction = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the camera
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

            // Sync the xRotation variable so the camera does not just snap when the player gets control back // DOES NOT SEEM TO WORK
            xRotation = transform.localEulerAngles.x;
            if (xRotation > 180f) xRotation -= 360f;

            return; 
        }

        HandleCameraLook();
    }

    /// <summary>
    /// Reads mouse input and applies rotation to the camera and player body.
    /// </summary>
    private void HandleCameraLook() 
    {
        Vector2 lookVector = gameInput.GetLookVector(); // Get input delta from GameInput

        // Scale by sensitivity and time
        float mouseX = lookVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookVector.y * mouseSensitivity * Time.deltaTime;

        // Vertical look
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX); // Horizontal look
    }
}
