using UnityEngine;

/// <summary>
/// Waits for the carIsFixed variable from INK and stops the engine smoke particles when true
/// </summary>
public class EngineSmokeController : MonoBehaviour // author : STefan Cwiek
{
    [Tooltip("smoke particle here")]
    [SerializeField] private ParticleSystem smoke;

    [Tooltip("The ink variable that the car is fixed here")]
    [SerializeField] private string inkVarName = "carIsFixed";

    private bool smokeStopped = false;

    private void Update()
    {
        // Dont check if smoke already stopped or no dialogue man. 
        if (smokeStopped || NewDialogueManager.Instance == null || NewDialogueManager.Instance.dialogueVariables == null)
            return;

        // find the ink variable
        if (NewDialogueManager.Instance.dialogueVariables.variables.TryGetValue(inkVarName, out Ink.Runtime.Object value))
        {
            if (value is Ink.Runtime.BoolValue boolVal && boolVal.value == true)
            {
                if (smoke != null)
                {
                    smoke.Stop(); 
                }
                smokeStopped = true;
            }
        }
    }
}
