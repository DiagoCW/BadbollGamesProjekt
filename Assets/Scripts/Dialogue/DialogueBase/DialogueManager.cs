using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public DialogueScript dialogueUI;

    private bool isDialogueActive = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (isDialogueActive) return;
        isDialogueActive = true;

        dialogueUI.StartDialogue(dialogue);
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}

