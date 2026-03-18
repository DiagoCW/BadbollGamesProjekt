using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueData dialogue;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueManager.Instance.IsDialogueActive())
            {
                DialogueManager.Instance.StartDialogue(dialogue);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Debug.Log("Player entered NPC range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // Debug.Log("Player left NPC range");
        }
    }
}