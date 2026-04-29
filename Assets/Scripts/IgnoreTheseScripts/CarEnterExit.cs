using UnityEngine;
using System;

public class CarEnterExit : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private GameInput gameInput;

    [Header("Player & Cameras")]
    [SerializeField] private GameObject playerObject; // The whole player character
    [SerializeField] private GameObject playerCamera; // The player's main camera
    [SerializeField] private GameObject carCamera;    // The camera attached to the car

    [Header("Car Components")]
    [SerializeField] private SimpleCarController carController;
    [SerializeField] private Transform getOutPosition; // Where the player spawns when exiting
    [SerializeField] private float interactDistance = 4f; // How close you must be to enter

    private bool isDriving = false;

    private void Start()
    {
        // Make sure the car is completely turned off by default
        if (carController != null) carController.enabled = false;
        if (carCamera != null) carCamera.SetActive(false);

        // Subscribe to your interact button (E)
        if (gameInput != null)
        {
            gameInput.OnInteractAction += GameInput_OnInteractAction;
        }
    }

    private void OnDestroy()
    {
        // Always unsubscribe!
        if (gameInput != null)
        {
            gameInput.OnInteractAction -= GameInput_OnInteractAction;
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (isDriving)
        {
            // If we are already driving, pressing E makes us exit!
            ExitCar();
        }
        else
        {
            // If we are on foot, are we close enough to the car to get in?
            float distanceToCar = Vector3.Distance(playerObject.transform.position, transform.position);

            if (distanceToCar <= interactDistance)
            {
                EnterCar();
            }
        }
    }

    private void EnterCar()
    {
        isDriving = true;

        // 1. Turn ON the car controls and car camera
        carController.enabled = true;
        carCamera.SetActive(true);

        // 2. Turn OFF the player and player camera
        playerObject.SetActive(false);
        playerCamera.SetActive(false);

        Debug.Log("Entered the car!");
    }

    private void ExitCar()
    {
        isDriving = false;

        // 1. Turn OFF the car controls and car camera
        carController.enabled = false;
        carCamera.SetActive(false);

        // 2. Move the invisible player to the door, then turn them ON
        playerObject.transform.position = getOutPosition.position;
        playerObject.transform.rotation = getOutPosition.rotation; // Face away from the car

        playerObject.SetActive(true);
        playerCamera.SetActive(true);

        Debug.Log("Exited the car!");
    }
}