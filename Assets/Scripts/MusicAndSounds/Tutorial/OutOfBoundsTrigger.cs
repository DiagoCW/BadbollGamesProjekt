using UnityEngine;

/// <summary>
/// Triggers a warning dialogue and audio cue when the player wanders out of bounds
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class OutOfBoundsTrigger : MonoBehaviour // Author: Stefan Cwiek
{
    [Header("Settings")]
    [Tooltip("The ink file containing the warning dialogue")]
    [SerializeField] private TextAsset warningDialogue;

    [Tooltip("The ID of the audio clip to play in")]
    [SerializeField] private string audioID;

    private string playerTag = "Player"; // The tag of the player object

    private bool isWarningActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isWarningActive) 
        {
            TriggerWarning(other.gameObject);
        }
    }

    private void TriggerWarning(GameObject player) 
    {
        isWarningActive = true;

        // play the sound
        if (StartAudioManager.Instance != null && !string.IsNullOrEmpty(audioID)) 
        {
            StartAudioManager.Instance.PlaySFX(audioID);
        }

        // play ink dialogue
        if (NewDialogueManager.Instance != null && warningDialogue != null) 
        {
            NewDialogueManager.Instance.EnterDialogue(warningDialogue, null, null);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag)) isWarningActive = false; // Reset trigger when player left the zone
    }

}
