using UnityEngine;
using Ink.Runtime;

/// <summary>
/// Triggers the Detective Vision tutorial only if the player has already inspected the engine puncture
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class VisionTutorialTrigger : MonoBehaviour // Author Stefan
{
    [Header("Dialogue Settings")]
    [Tooltip("The ink file containing the D Vision tutorial")]
    public TextAsset tutorialDialogue;

    [Tooltip("The ink variable that must be true for this trigger to work.")]
    public string requiredInkVariable = "inspectedHole";

    [Tooltip("player object tag")]
    public string playerTag = "Player";

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Ignore if already triggered or not player
        if (hasTriggered || !other.CompareTag(playerTag)) return;

        // Ensure DialogueManager is active 
        if (NewDialogueManager.Instance != null && NewDialogueManager.Instance.currentStory != null)
        {
            // Read the variable from Ink variable state
            var variableValue = NewDialogueManager.Instance.currentStory.variablesState[requiredInkVariable];

            // If variable exists and is true, play the tutorial
            if (variableValue != null && (bool)variableValue == true)
            {
                hasTriggered = true; // Lock the trigger so it never fires again
                NewDialogueManager.Instance.EnterDialogue(tutorialDialogue, null, null);
            }
        }
    }
}