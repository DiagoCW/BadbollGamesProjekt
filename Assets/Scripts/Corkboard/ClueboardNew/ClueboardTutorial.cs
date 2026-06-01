using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Stefan Cwiek
/// 
/// Handles showing and hiding the clueboard instructions panel
/// </summary>
public class ClueboardTutorial : MonoBehaviour
{
    [Header("UI references")]
    [Tooltip("The main tutorial box that contains the background and TMP text")]
    public GameObject tutorialBox;

    [Tooltip("The UI object that tells the player to open the instructions")]
    public GameObject promptUI;

    [Header("State Checking")]
    [Tooltip("Reference to the board trigger to check if the player is looking at the board")]
    public ClueboardTrigger boardTrigger;

    [Header("Input")]
    [Tooltip("The key the player needf to press to open/close the instructions")]
    private KeyCode toggleKey = KeyCode.I;

    private void OnEnable()
    {
        if (tutorialBox != null) 
        {
            tutorialBox.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the board is closed
        if (boardTrigger != null && !boardTrigger.isViewingBoard)
        {
            // Hide the prompt, hide the tutorial, and ignore keyboard inputs
            if (promptUI != null && promptUI.activeSelf) promptUI.SetActive(false);
            if (tutorialBox != null && tutorialBox.activeSelf) tutorialBox.SetActive(false);

            return;
        }

        // If the board is open, make the "Press " prompt visible 
        if (promptUI != null && !promptUI.activeSelf)
        {
            promptUI.SetActive(true);
        }

        // listen for the player pressing T
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleTutorial();
        }
    }

    /// <summary>
    /// Flips the active state of the tutorial box
    /// </summary>
    public void ToggleTutorial() 
    {
        if (tutorialBox != null) 
        {
            tutorialBox.SetActive(!tutorialBox.activeSelf);
        }
    }
}
