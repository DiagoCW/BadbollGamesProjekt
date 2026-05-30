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

    private float xRotation = 0f; // Tracks up/down rotation state to allow for clamping.
   // private float yRotation = 0f; // Tracks left/right rotation state

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        // Prevent camera movement if the inventory is open so the player can use their mouse
        if (PlayerController.Instance != null && PlayerController.Instance.IsInventoryOpen)
        {
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
