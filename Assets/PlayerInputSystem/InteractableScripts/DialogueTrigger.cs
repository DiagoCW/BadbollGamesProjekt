using UnityEngine;
using Ink.Runtime;
using Ink.Parsed;
using Unity.VisualScripting;
using UnityEngine.AI;
using NUnit.Framework;

/// <summary>
/// Author: Isak
/// The most basic script for starting dialogue with characters or objects. 
/// Implements an interface that allows the player to call the interface method when interacting with the object.
/// The method then simply calls the EnterDialogue() method, and passes along arguments for their animator component
/// as well as NavMesh-script for controlling movement. 
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

    Animator npcAnimator;
    TestAIScript aiScript;
    
    void Awake()
    {
        npcDir = this.transform;
        initialNpcDir = npcDir;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        npcAnimator = GetComponentInChildren<Animator>();
        aiScript = GetComponentInChildren<TestAIScript>();
    }

    public void Interact()
    {
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

    // Rotates the NPC within range to face the player
    public void FacePlayer()
    {
        if (aiScript != null && aiScript.isMoving) return; // returns if they are currently moving, as to not face the player
        Vector3 rotation = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(rotation);
    }
}
