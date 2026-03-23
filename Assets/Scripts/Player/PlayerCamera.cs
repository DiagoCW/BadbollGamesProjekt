using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform playerBody;

    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 15f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        HandleCameraLook();
    }

    private void HandleCameraLook() 
    {
        Vector2 lookVector = gameInput.GetLookVector();

        float mouseX = lookVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookVector.y * mouseSensitivity * Time.deltaTime;

        // Vertical look
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal look
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
