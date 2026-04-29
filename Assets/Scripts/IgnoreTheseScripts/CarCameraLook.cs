using UnityEngine;

public class CarCameraLook : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private GameInput gameInput;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 1.5f;
    [SerializeField] private float upDownLimit = 60f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private Quaternion startingRotation;

    private void Awake()
    {
        startingRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        xRotation = 0f;
        yRotation = 0f;

        transform.localRotation = startingRotation;
    }

    private void Update()
    {
        if (gameInput == null) return;

        Vector2 lookInput = gameInput.GetLookVector();

        yRotation += lookInput.x * mouseSensitivity;
        xRotation -= lookInput.y * mouseSensitivity;

        xRotation = Mathf.Clamp(xRotation, -upDownLimit, upDownLimit);

        transform.localRotation = startingRotation * Quaternion.Euler(xRotation, yRotation, 0f);
    }
}