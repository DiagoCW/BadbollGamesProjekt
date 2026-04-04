using UnityEngine;
using Ink.Runtime;
using Ink.Parsed;

/// <summary>
/// Detta script läggs till på alla komponenter som ska kunna trigga igång dialog,
/// gäller både NPCs och andra objekt som spelaren kan interagera med.
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [Header("Transform NPC & Player")]
    Transform npcDir;
    Transform player;
    //public static Animator npcAnimator { get; set; }
    //Transform defaultPos;
    
    // Ink JSON fil som håller dialogen som objektet ska visa vid interaktion
    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJson;
    
    void Awake()
    {
        npcDir = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //NewDialogueManager.Instance.currentStory.variablesState["foundSnusdosa"] = true;
        
    }

    public void Interact()
    {
        Debug.Log(inkJson.text);
        NewDialogueManager.Instance.EnterDialogue(inkJson, this.gameObject);
        //FacePlayer();
    }

    void Update()
    {
        if (gameObject.CompareTag("NPC"))
        {
            if (NewDialogueManager.Instance.dialogueIsPlaying ||
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
