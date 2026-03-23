using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 30f;
    [SerializeField] Transform playerCamera;
    float xRotation, yRotation;
    float mouseX, mouseY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputValue value)
    {
        mouseX = value.Get<Vector2>().x * mouseSensitivity * Time.deltaTime;
        mouseY = value.Get<Vector2>().y * mouseSensitivity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        xRotation = Mathf.Clamp(xRotation - mouseY, -35f, 40f);
        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
