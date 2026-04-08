using UnityEngine;
using Ink.Runtime;
using Ink.Parsed;

/// <summary>
/// Detta script läggs till på alla NPCs som spelaren ska kunna interagera med och starta dialog.
/// Den innehåller en JSON-fil med dess Inkdialog som skickas till DialogueManager.
/// 
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [Header("Transform NPC & Player")]
    Transform npcDir;
    Transform player;
    
    // Ink JSON fil som håller dialogen som objektet ska visa vid interaktion
    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJson;

    Animator npcAnimator;
    
    void Awake()
    {
        npcDir = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //NewDialogueManager.Instance.currentStory.variablesState["foundSnusdosa"] = true;
        
        npcAnimator = GetComponentInChildren<Animator>();
    }

    public void Interact()
    {
        //gameObject.tag = "Untagged";
        NewDialogueManager.Instance.EnterDialogue(inkJson, npcAnimator);
    }

    void Update()
    {
        if (gameObject.CompareTag("NPC"))
        {
            if (/*NewDialogueManager.Instance.dialogueIsPlaying ||*/
            Vector3.Distance(npcDir.position, player.position) < 5)
                FacePlayer();
        }
    }

    public void FacePlayer()
    {
        Vector3 rotation = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(rotation);
    }
}
