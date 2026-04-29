using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCarController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private GameInput gameInput; 

    [Header("Car Settings")]
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float turnSpeed = 100f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = new Vector3(0, -1f, 0);
    }

    private void FixedUpdate()
    {
        if (gameInput == null) return;

        // Get your WASD / Joystick input
        Vector2 input = gameInput.GetMovementVectorNormalized();

        float gasPedal = input.y;   // W and S
        float steeringWheel = input.x; // A and D

        // 1. Move Forward / Backward
        Vector3 forwardMove = transform.forward * gasPedal * moveSpeed;

        // Apply the movement, but keep the current Y velocity so gravity still works!
        rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);

        // 2. Steer Left / Right
        // We only want to steer if the car is actually moving
        if (Mathf.Abs(gasPedal) > 0.1f)
        {
            float direction = Mathf.Sign(gasPedal);

            float turn = steeringWheel * turnSpeed * direction * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}