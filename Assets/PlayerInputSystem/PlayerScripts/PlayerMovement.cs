using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 movementInput;
    Vector3 movementVector;
    [SerializeField] float movementSpeed = 150f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            //Debug.Log(value);
            rb.AddForce(new Vector3(0, 100, 0));
        }
    }

    public void OnMovement(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        movementInput = new Vector3
            (inputVector.x, 0, inputVector.y);
    }

    void ApplyMovement()
    {
        movementVector = movementInput.x * transform.right +
            movementInput.z * transform.forward;
        movementVector.y = 0;
        rb.linearVelocity = movementVector * movementSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        ApplyMovement();
    }
}

