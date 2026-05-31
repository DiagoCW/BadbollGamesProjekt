using UnityEngine;

/// <summary>
/// Listens to the ink variable to trigger the end of the game. Fades out and loads the credits scene
/// </summary>
public class FinishGameTrigger : MonoBehaviour // Author: Stefan Cwiek
{
    [Header("end game settings")]
    [Tooltip("Ink variable that ends the whole game.")]
    [SerializeField] private string inkVariable = "";

    [Tooltip("The name of the credits scene")]
    [SerializeField] private string creditsSceneName = "Credits";

    [Tooltip("How long the screen takes to fade out")]
    [SerializeField] private float fadeDuration = 5.0f;

    private string playerTag = "Player";

    private bool isEnding = false;

    private void Update()
    {
        // skip if already ending or dialogue stil playing
        if (string.IsNullOrEmpty(inkVariable) || isEnding || NewDialogueManager.Instance == null || NewDialogueManager.Instance.dialogueIsPlaying)
            return;

        bool shouldEnd = false;

        // Check ink var
        if (NewDialogueManager.Instance.dialogueVariables.variables.TryGetValue(inkVariable, out Ink.Runtime.Object value)) 
        {
            if (value is Ink.Runtime.BoolValue boolVal) 
            {
                shouldEnd = boolVal.value;
            }
        }

        if (shouldEnd) 
        {
            isEnding = true;

            // turn off player controls
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player != null) 
            {
                PlayerController pc = player.GetComponent<PlayerController>();
                if (pc != null) pc.enabled = false;
            }

            if (FadeInOut.Instance != null) 
            {
                FadeInOut.Instance.FadeToScene(creditsSceneName, fadeDuration);
            }
        }
    }
}
