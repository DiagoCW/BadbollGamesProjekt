using UnityEngine;

/// <summary>
/// Author: Isak
/// Empty gameobjects that should trigger certain dialogue when the player enters / stays in the gameObject, and its 
/// condition is true. The condition is determined by a global variable within INK, and the script will check 
/// everytime the trigger gameobject is entered by the player if the condition is true.
/// A trigger is often destroyed once it has been triggered once, but sometimes a trigger has to be recurring, i.e
/// when the player tries to reach an area they can't go to.
/// The name of the trigger that the script should check for is manually entered in the Inspector of the gameobject. 
/// If this space is left empty, then the trigger will default to always be true.
/// </summary>
public class TriggerTutorial : MonoBehaviour
{
    [SerializeField] TextAsset inkJson, inkJsonTest;
    [SerializeField] string Inkvariable;
    object trigger = false;
    void Start()
    {
        // Some triggers that should start at the start of the scene won't execute, since an INK file must have already
        // been read by the DialogueManager to instantiate the variables. To fix this, an empty INK JSON file is processed
        // before calling the actual INK file, allowing it to check the correct variables.
        if (inkJsonTest != null)
            NewDialogueManager.Instance.EnterDialogue(inkJsonTest, null, null);
        if (string.IsNullOrEmpty(Inkvariable))
            trigger = true;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if ((bool)trigger && string.IsNullOrEmpty(Inkvariable))
        {
            Animator anim = GameObject.Find("StoreClerk").GetComponentInChildren<Animator>();
            TestAIScript aiScript = GameObject.Find("StoreClerk").GetComponent<TestAIScript>();

            Debug.Log($"Activated function for {anim.name}, {aiScript.name}");
            NewDialogueManager.Instance.EnterDialogue(inkJson, anim, aiScript);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (NewDialogueManager.Instance.dialogueIsPlaying || string.IsNullOrEmpty(Inkvariable))
            return;

        Debug.Log($"Globals variablename: {NewDialogueManager.Instance.dialogueVariables.variables.ContainsKey(Inkvariable)}");
        Debug.Log($"Trigger: {trigger}");

        if (!string.IsNullOrEmpty(Inkvariable))
            trigger =
                NewDialogueManager.Instance.dialogueVariables.variables.TryGetValue(Inkvariable, out Ink.Runtime.Object value);

        //trigger = (Ink.Runtime.BoolValue)
        //  NewDialogueManager.Instance.GetVariableState(Inkvariable);

        if (other.CompareTag("Player") && (bool)trigger
            && !NewDialogueManager.Instance.dialogueIsPlaying)
        {
            NewDialogueManager.Instance.EnterDialogue(inkJson, null, null);
            Debug.Log($"Trigger {Inkvariable} activated");
        }

        if ((bool)trigger && !string.IsNullOrEmpty(Inkvariable))
        {
            Debug.Log($"Destroying trigger " + Inkvariable);
            Destroy(gameObject);
        }
    }
}
