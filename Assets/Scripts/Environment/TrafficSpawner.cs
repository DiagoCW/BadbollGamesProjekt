using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Stefan Cwiek
/// 
/// Handles the periodic spawning of traffic vehicles at a specific point.
/// Makes sure that vehicles are only spawned if the area is clear to prevent overlapping
/// </summary>
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
        // Begin the spawning loop when the script is initialized
        StartCoroutine(SpawnTrafficRoutine());
    }

    /// <summary>
    /// A coroutine that attempts to spawn a vehicle every spawnInterval
    /// </summary>
    private IEnumerator SpawnTrafficRoutine()
    {
        while (true)
        {
            SpawnVehicle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// Attempts to spawn a random vehicle prefab from the array.
    /// </summary>
    private void SpawnVehicle()
    {
        // Safety check to make sure we have prefabs to spawn and start/end points assigned
        if (vehiclePrefabs.Length == 0 || spawnPoint == null || endPoint == null)
            return;

        // Safety check to not spawn if there is already a vehicle in the spawn radius
        if (Physics.CheckSphere(spawnPoint.position, spawnCheckRadius, vehicleLayerMask))
            return;

        // Pick a random vehicle prefab from the array
        int randomIndex = Random.Range(0, vehiclePrefabs.Length);
        GameObject selectedPrefab = vehiclePrefabs[randomIndex];

        // Instantiate the vehicle at the position and rotation of the spawn point
        GameObject newVehicle = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);

        // If the spawned prefab has a CarAi component, assign it to the destination route
        if (newVehicle.TryGetComponent(out CarAI carAI)) 
        {
            carAI.SetRoute(endPoint);
        }
    }
}
