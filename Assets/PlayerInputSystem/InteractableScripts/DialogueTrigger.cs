using UnityEngine;
using Ink.Runtime;
using Ink.Parsed;

/// <summary>
/// Detta script läggs till på alla komponenter som ska kunna trigga igång dialog,
/// gäller både NPCs och andra objekt som spelaren kan interagera med.
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    // Ink JSON fil som håller dialogen som objektet ska visa vid interaktion
    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJson;
    
    public void Interact()
    {
        Debug.Log(inkJson.text);
        NewDialogueManager.Instance.EnterDialogue(inkJson);
    }
}
