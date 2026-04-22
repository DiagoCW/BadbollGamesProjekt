using UnityEngine;

public class CarAI : MonoBehaviour
{
    [Header("Driving Settings")]
    [SerializeField] private float maxDriveSpeed = 10f;
    [SerializeField] private float acceleration = 5f;

    [Header("Detection Settings")]
    [SerializeField] private float detectionDistance = 5f;
    [SerializeField] private float stoppingDistance = 1.5f;
    [SerializeField] private LayerMask obstacleLayerMask;

    [Header("Sensors")]
    [SerializeField] private Transform sensorOrigin;

    [Tooltip("Half the size of the detection box (X=Width, Y=Height, Z=Length). Increase X for wider vehicles like buses.")]
    [SerializeField] private Vector3 boxHalfExtents = new Vector3(1.5f, 1f, 0.5f);

    private Transform endPoint;
    private float currentSpeed;
    private bool isDriving = false;

    public void SetRoute(Transform targetEnd)
    {
        endPoint = targetEnd;
        isDriving = true;
    }

    private void Update()
    {
        if (!isDriving) return;

        CalculateSpeed();
        DriveForward();
    }

    private void CalculateSpeed()
    {
        if (sensorOrigin == null) return;

        RaycastHit[] hits = Physics.BoxCastAll(
            sensorOrigin.position,
            boxHalfExtents,
            sensorOrigin.forward,
            sensorOrigin.rotation,
            detectionDistance,
            obstacleLayerMask
        );

        float closestDistance = Mathf.Infinity;
        bool foundObstacle = false;
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.transform.root == this.transform.root)
            {
                continue; 
            }

            if (hit.distance < closestDistance)
            {
                closestDistance = hit.distance;
                foundObstacle = true;
            }
        }

        if (foundObstacle)
        {
            if (closestDistance <= stoppingDistance)
            {
                currentSpeed = 0f; 
            }
            else
            {
                float targetSpeed = maxDriveSpeed * (closestDistance / detectionDistance);
                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxDriveSpeed, Time.deltaTime * acceleration);
        }
    }

    private void DriveForward()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (endPoint != null)
        {
            float distanceToTarget = Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(endPoint.position.x, 0, endPoint.position.z)
            );

            if (distanceToTarget < 2f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if ((!Application.isPlaying || isDriving) && sensorOrigin != null)
        {
            // Save the original world matrix to not break other gizmos
            Matrix4x4 oldMatrix = Gizmos.matrix;

            // Aligns the gizmo to the sensor's position and rotation
            Gizmos.matrix = Matrix4x4.TRS(sensorOrigin.position, sensorOrigin.rotation, Vector3.one);

            Vector3 sweepCenter = new Vector3(0f, 0f, detectionDistance / 2f);
            Vector3 sweepSize = new Vector3(boxHalfExtents.x * 2f, boxHalfExtents.y * 2f, detectionDistance + (boxHalfExtents.z * 2f));

            // Draws a box to see the volume 
            Gizmos.color = new Color(0f, 1f, 1f, 0.2f); // 20% transparent cyan
            Gizmos.DrawCube(sweepCenter, sweepSize);

            // Draw the edges
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(sweepCenter, sweepSize);

            // Put the matrix back
            Gizmos.matrix = oldMatrix;
        }
    }
}