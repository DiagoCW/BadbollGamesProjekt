using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private GameObject[] vehiclePrefabs; // Array of cars
    [SerializeField] private float spawnInterval = 6f; // Time between spawns

    [Header("Pathing")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;

    [Header("Spawn Safety Checks")]
    [SerializeField] private float spawnCheckRadius = 5f; // Radius to check for existing vehicles
    [SerializeField] private LayerMask vehicleLayerMask; // Layer mask for vehicles

    private void Start()
    {
        StartCoroutine(SpawnTrafficRoutine());
    }

    private IEnumerator SpawnTrafficRoutine()
    {
        while (true)
        {
            SpawnVehicle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnVehicle()
    {
        if (vehiclePrefabs.Length == 0 || spawnPoint == null || endPoint == null)
            return;

        if (Physics.CheckSphere(spawnPoint.position, spawnCheckRadius, vehicleLayerMask))
            return;

        int randomIndex = Random.Range(0, vehiclePrefabs.Length);
        GameObject selectedPrefab = vehiclePrefabs[randomIndex];

        GameObject newVehicle = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);

        if (newVehicle.TryGetComponent(out CarAI carAI)) 
        {
            carAI.SetRoute(endPoint);
        }
    }
}
