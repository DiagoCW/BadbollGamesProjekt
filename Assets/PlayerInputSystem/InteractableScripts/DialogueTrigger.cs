using UnityEngine;
using Ink.Runtime;
using Ink.Parsed;
using Unity.VisualScripting;
using UnityEngine.AI;
using NUnit.Framework;

/// <summary>
/// Detta script läggs till på alla NPCs som spelaren ska kunna interagera med och starta dialog.
/// Den innehåller en JSON-fil med dess Inkdialog som skickas till DialogueManager.
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [Header("Transform NPC & Player")]
    Transform npcDir;
    Transform initialNpcDir;
    Transform player;
    
    // Ink JSON fil som håller dialogen som objektet ska visa vid interaktion
    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJson;
    //[SerializeField] public string npcName;
    
    /*[SerializeField]*/ HighlightActivatorIAVersion highlighter;

    Animator npcAnimator;
    TestAIScript aiScript;
    
    void Awake()
    {
        npcDir = this.transform;
        initialNpcDir = npcDir;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //NewDialogueManager.Instance.currentStory.variablesState["foundSnusdosa"] = true;
        //highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();
        npcAnimator = GetComponentInChildren<Animator>();
        aiScript = GetComponentInChildren<TestAIScript>();
    }

    void Start()
    {
        
    }

    public void Interact()
    {
        //if (highlighter.IsHighlighting) return;
        NewDialogueManager.Instance.EnterDialogue(inkJson, npcAnimator, aiScript);
    }

    void Update()
    {
        if (gameObject.CompareTag("NPC") && gameObject.name != "ArmchairGuy")
        {
            if (/*NewDialogueManager.Instance.dialogueIsPlaying ||*/
            Vector3.Distance(npcDir.position, player.position) <= PlayerController.Instance.InteractRange() + 0.5f)
                FacePlayer();
            //else
            //{
            //    Vector3 rotation = new Vector3(-transform.position.x, transform.position.y, -transform.position.y);
            //    transform.LookAt(rotation);
            //    return;
            //}
            
        }
        
    }

    public void FacePlayer()
    {
        if (aiScript != null && aiScript.isMoving) return;
        Vector3 rotation = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(rotation);
    }
}
