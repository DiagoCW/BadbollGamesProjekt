using UnityEngine;

/// <summary>
/// Author: Isak
/// A more advanced script for triggering dialogue, commonly used for Items that are obtainable or otherwise needs to
/// check a certain condition if they can be interacted with, often through the use of Detective Vision.
/// Defaults to dialogue with the first INK file, and once a condition is met it instead displays the second INK file. 
/// Holds a reference to the Outline Highlighter script to determine whether the object has been highlighted or not.
/// Also holds item objects that can be obtained by the player. 
/// </summary>
public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("References")]
    private InventoryObject playerInventory;
    [SerializeField] ItemObject item, item2;
    [SerializeField] TextAsset inkJson, inkJson2;

    [Tooltip("The name of the NPC that should pass their animator and AIScript when interacting with an item.")]
    [SerializeField] string NPCName;

    [Header("Settings")]
    [Tooltip("Should this object be destroyed when picked up?")]
    [SerializeField] private bool destroyOnPickup = false;

    bool pickedUpClue = false;
    
    HighlightActivatorIAVersion highlighter;
    ParticleSystem particles;
    OutlineHighlighter outline;

    void Start()
    {
        playerInventory = PlayerController.Instance.inventory;
        highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();
        outline = GetComponentInChildren<OutlineHighlighter>();

    }
    public void Interact()
    {
        if (outline != null && outline.hasBeenHighlighted) //highlighter.IsHighlighting)
        {
            //Debug.Log("Detective vision enabled, and item is interacted with");
            if (inkJson2 != null)
            {
                Animator anim = null;
                TestAIScript aiScript = null;
                if (!string.IsNullOrEmpty(NPCName))
                {
                    anim = GameObject.Find(NPCName).GetComponentInChildren<Animator>();
                    aiScript = GameObject.Find(NPCName).GetComponent<TestAIScript>();
                }
                NewDialogueManager.Instance.EnterDialogue(inkJson2, anim, aiScript);
                gameObject.tag = "Untagged";
            }

            if (!pickedUpClue && item != null)
            {
                playerInventory.AddItem(new Item(item));
                pickedUpClue = true;
                gameObject.tag = "Untagged"; // removes "clue" tag so that object no longer can be highlighted

                if (destroyOnPickup) Destroy(gameObject); // If box checked, destroy the object when its been picked up
            }
        }
        else if (inkJson != null && !highlighter.IsHighlighting)
            NewDialogueManager.Instance.EnterDialogue(inkJson, null, null);
        //else Debug.Log("Detective vision NOT enabled, and item is interacted with");
    }
}
