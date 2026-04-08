using UnityEngine;

/// <summary>
/// Används för att sätta igång triggers baserade på ändrade variabler i GlobalsMain.Ink;
/// t.ex startas en trigger när dVisionTutorialTrigger sätts 
/// </summary>
public class TriggerTutorial : MonoBehaviour
{
    [SerializeField] TextAsset inkJson;
    [SerializeField] string Inkvariable = string.Empty;
    object trigger = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Trigger: " + Inkvariable + " " + "value: " + trigger);
    }

    private void OnTriggerEnter(Collider other)
    {

        //trigger = (Ink.Runtime.BoolValue)
        //NewDialogueManager.Instance.GetVariableState("dvisionTutorialTrigger");
    }

    private void OnTriggerStay(Collider other)
    {
        if (NewDialogueManager.Instance.dialogueIsPlaying)
            return;
        trigger =
            NewDialogueManager.Instance.dialogueVariables.variables.TryGetValue(Inkvariable, out Ink.Runtime.Object value);

        if (other.CompareTag("Player") && (bool)trigger 
            && !NewDialogueManager.Instance.dialogueIsPlaying)
        {
            NewDialogueManager.Instance.EnterDialogue(inkJson, null);
            Debug.Log($"Trigger {Inkvariable} activated");
        }
        
        if ((bool)trigger)
        {
            Debug.Log($"Destroying trigger " + Inkvariable);
            Destroy(gameObject);
        }
            
    }
}
