using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    [SerializeField] private float minPitch = -80f;    // looking down limit
    [SerializeField] private float maxPitch = 80f;     // looking up limit

    private float xRotation = 0f;   // current vertical angle

    void Start()
    {
        // Hide & lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical look (pitch)
        xRotation -= mouseY;                    // minus because mouse down = look up
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);

        // Apply vertical rotation to camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal look rotate whole player 
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}