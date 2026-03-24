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

    private Transform endPoint;
    private Vector3 boxHalfExtents = new Vector3(1.5f, 1f, 0.5f);
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
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(sensorOrigin.position, sensorOrigin.forward * detectionDistance);
            Gizmos.DrawWireCube(sensorOrigin.position + sensorOrigin.forward * detectionDistance, boxHalfExtents * 2f);
        }
    }
}