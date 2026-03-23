using UnityEngine;

public class CarAI : MonoBehaviour
{
    [Header("Driving Settings")]
    [SerializeField] private float driveSpeed = 10f;
    [SerializeField] private float detectionDistance = 5f;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("Pathing")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    // size of the detection box in front of the car
    private Vector3 boxHalfExtents = new Vector3(1f, 1f, 0.5f);
    private bool isWaitingForPlayer;

    private void Update()
    {
        CheckForPlayer();

        if (!isWaitingForPlayer)
        {
            DriveForward();
        }
    }

    private void CheckForPlayer()
    {
        // casts a box forward from the car position to detect the player layer
        isWaitingForPlayer = Physics.BoxCast(
            transform.position + Vector3.up, 
            boxHalfExtents,
            transform.forward,
            transform.rotation,
            detectionDistance,
            playerLayerMask
        );
    }

    private void DriveForward()
    {
        // move the car forward
        transform.Translate(Vector3.forward * driveSpeed * Time.deltaTime);

        // teleport back to start if it reaches the end 
        if (startPoint != null && endPoint != null)
        {
            if (Vector3.Distance(transform.position, endPoint.position) < 1f)
            {
                transform.position = startPoint.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //visualizes the detection box in the Scene view
        Gizmos.color = isWaitingForPlayer ? Color.red : Color.green;
        Vector3 startPos = transform.position + Vector3.up;

        // Draw the ray and the box
        Gizmos.DrawRay(startPos, transform.forward * detectionDistance);
        Gizmos.DrawWireCube(startPos + transform.forward * detectionDistance, boxHalfExtents * 2f);
    }
}