using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Stefan Cwiek
/// 
/// Starts a countdown when triggered, then deletes the gameobject it is attached to 
/// </summary>
public class TimedSelfDestruct : MonoBehaviour
{
    [Tooltip("Seconds before the object self destructs after trigger")]
    public float delayInSeconds = 2f;
    [Tooltip("The ink bool variable to trigger it")]
    [SerializeField] private string inkVariableName = "";

    bool isTriggered = false;

    private void Update()
    {
        // Skip if these things
        if (string.IsNullOrEmpty(inkVariableName) || isTriggered || NewDialogueManager.Instance == null)
            return;

        bool shouldDestroy = false;

        // Check ink var
        if (NewDialogueManager.Instance.dialogueVariables.variables.TryGetValue(inkVariableName, out Ink.Runtime.Object value))
        {
            if (value is Ink.Runtime.BoolValue boolVal)
            {
                shouldDestroy = boolVal.value;
            }
        }

        // when ink var true
        if (shouldDestroy)
        {
            isTriggered = true; // Lock so doesnt loop

            // Start countdown
            StartCoroutine(DestroyYourself());
        }
    }

    private IEnumerator DestroyYourself()
    {
        // Wait for the requested time
        yield return new WaitForSeconds(delayInSeconds);

        // Delete this object from the scene
        Destroy(gameObject);
    }
}
