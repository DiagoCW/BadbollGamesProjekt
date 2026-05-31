using UnityEngine;
using System.Collections;

/// <summary>
/// Fades the screen to black, teleports the player to a new location within the same scene
/// Can be triggered physically (boxcollider) ORRRR via ink variable
/// Optionally plays dialogue upon arrival
/// </summary>
public class LocalTeleporter : MonoBehaviour // Author: Stefan Cwiek
{
    [Header("Teleport settings")]
    [Tooltip("empty object where you want the player to")]
    [SerializeField] private Transform destination;

    [Tooltip("How long the screen takes to fade out and in")]
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("Ink settings (optional)")]
    [Tooltip("If you want dialogue to trigger this. Type the Ink variable name here")]
    [SerializeField] private string inkVariableName = "";

    [Tooltip("Ink file to play when the player arrives (optional)")]
    [SerializeField] private TextAsset arrivalDialogue;

    private string playerTag = "Player";

    private bool isTeleporting = false; // prevent coroutine from firing multiple times

    private void Update()
    {
        // skip all this if no ink variable is assigned, if we are already teleporting, or if dialogue still typing
        if (string.IsNullOrEmpty(inkVariableName) || isTeleporting || NewDialogueManager.Instance == null || NewDialogueManager.Instance.dialogueIsPlaying)
            return;

        bool shouldTeleport = false;

        if (NewDialogueManager.Instance.dialogueVariables.variables.TryGetValue(inkVariableName, out Ink.Runtime.Object value)) // look up the ink VAR
        {
            if (value is Ink.Runtime.BoolValue boolVal) 
            {
                shouldTeleport = boolVal.value;
            }
        }

        if (shouldTeleport) 
        {
            // Hämta värdet av TotalTimeUsed från HighlightActivator, och lagra det i ink variabeln som ska hålla mängden tid
            Ink.Runtime.Object dvisionValue = 
                new Ink.Runtime.IntValue((int)GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>().TotalTimeUsed);
            NewDialogueManager.Instance.dialogueVariables.variables["dvisionTotalTime"] = dvisionValue;

            if (NewDialogueManager.Instance.currentStory != null) // Set ink variable back to false so the player doesnt get stuck in teleport loop
            {
                NewDialogueManager.Instance.currentStory.variablesState[inkVariableName] = false;
            }

            // FORCE IT TO NEVER LOOP !!!
            if (NewDialogueManager.Instance.dialogueVariables != null && NewDialogueManager.Instance.dialogueVariables.variables.ContainsKey(inkVariableName)) 
            {
                NewDialogueManager.Instance.dialogueVariables.variables[inkVariableName] = new Ink.Runtime.BoolValue(false);
            }

            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if(player != null) 
            {
                StartCoroutine(TeleportSequence(player));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isTeleporting && destination != null) // for physical triggers
        {
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    private IEnumerator TeleportSequence(GameObject player) 
    {
        isTeleporting = true;

        PlayerController pc = player.GetComponent<PlayerController>(); // player cannot move during teleport fade out
        if (pc != null) pc.enabled = false;

        if (FadeInOut.Instance != null) 
        {
            FadeInOut.Instance.FadeScreenOnly(1f, fadeDuration); // Tell the FadeInOut singleton to fade to black
        }

        yield return new WaitForSeconds(fadeDuration); // Wait for the duration of the fade so the screen is black

        CharacterController cc = player.GetComponent<CharacterController>(); // Disable cc before moving
        if (cc != null) cc.enabled = false;

        // move and rotate the player
        player.transform.position = destination.position;
        player.transform.rotation = destination.rotation;

        if (cc != null) cc.enabled = true; // turn cc back on

        yield return new WaitForSeconds(0.1f); // wait a short moment for things to settle at new location

        if (FadeInOut.Instance != null) 
        {
            FadeInOut.Instance.FadeScreenOnly(0f, fadeDuration); // fade back to clear
        }

        isTeleporting = false;

        if (pc != null) pc.enabled = true; // player can move again

        if (arrivalDialogue != null && NewDialogueManager.Instance != null) 
        {
            NewDialogueManager.Instance.EnterDialogue(arrivalDialogue, null, null);
        }
    }

}
