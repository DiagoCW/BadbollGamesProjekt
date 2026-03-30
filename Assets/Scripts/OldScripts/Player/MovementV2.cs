using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    float yVelocity = 0f;
    bool grounded;

    void Start()
    {

    }

    void Update()
    {
        // Movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Ground check
        if (transform.position.y <= 1f)
        {
            grounded = true;
            yVelocity = 0f;

            // Snap back to ground level
            Vector3 pos = transform.position;
            pos.y = 1f;
            transform.position = pos;
        }
        else
        {
            grounded = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            yVelocity = jumpForce;
        }

        // Gravity
        yVelocity -= 20f * Time.deltaTime;

        // Apply vertical movement
        transform.Translate(Vector3.up * yVelocity * Time.deltaTime);
    }

}
