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
    [Tooltip("How fast camera pans to target and back")]
    [SerializeField] private float cinematicPanSpeed = 5f;

    private float xRotation = 0f; // Tracks up/down rotation state to allow for clamping.
   // private float yRotation = 0f; // Tracks left/right rotation state

    private Transform cinematicTarget;

    // State tracking of camera state
    private enum CameraState { Free, Cinematic, Returning}
    private CameraState currentState = CameraState.Free;

    // Store camera location before looking at the "cinematic" camera
    private Quaternion storedLocalRotation;
    private float storedXrotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetCinematicTarget(Transform target)
    {
        cinematicTarget = target;

        if (target != null) 
        {
            if (currentState == CameraState.Free) // store rotation when going from free look
            {
                storedLocalRotation = transform.localRotation;
                storedXrotation = xRotation;
            }
            currentState = CameraState.Cinematic;
        }
        else 
        {
            if (currentState == CameraState.Cinematic) 
            {
                currentState = CameraState.Returning;
            }
        }

    }

    private void LateUpdate()
    {
        // Prevent camera movement if the inventory is open so the player can use their mouse
        if (PlayerController.Instance != null && PlayerController.Instance.IsInventoryOpen)
        {
            return;
        }

        if (currentState == CameraState.Cinematic) // Looking at NPC
        {
            if (cinematicTarget == null) 
            {
                currentState = CameraState.Returning;
                return;
            }
            
            Vector3 targetPosition = cinematicTarget.position + (Vector3.up * cinematicHeightOffset);
            // Calculate the direction to the adjusted target position
            Vector3 direction = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the camera
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, cinematicPanSpeed * Time.deltaTime);

            return; 
        }

        // When returning to OG view
        if (currentState == CameraState.Returning)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, storedLocalRotation, cinematicPanSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.localRotation, storedLocalRotation) < 0.5f) 
            {
                transform.localRotation = storedLocalRotation;
                xRotation = storedXrotation;

                currentState = CameraState.Free;
            }
            return;
        }

        HandleCameraLook(); // Free, OG, Look
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
