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
    [Tooltip("Drag any clues that are obtainable through interaction with this object here")]
    [SerializeField] ItemObject item, item2;
    [Tooltip("Drag any INK-files here that should trigger depending on if you're interacting with it with or without Detective Vision enabled")]
    [SerializeField] TextAsset inkJson, inkJson2;

    [Tooltip("The name of the NPC that should pass their animator and AIScript when interacting with an item. " +
        "Used when characters should move or animate outside of interacting with the item.")]
    [SerializeField] string NPCName;

    [Header("Settings")]
    [Tooltip("Should this object be destroyed when picked up?")]
    [SerializeField] private bool destroyOnPickup = false;

    bool pickedUpClue = false;

    InventoryObject playerInventory; // Reference to the player inventory for adding clues 
    HighlightActivatorIAVersion highlighter;
    OutlineHighlighter outline;

    void Start() 
    {
        playerInventory = PlayerController.Instance.inventory;
        highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();
        outline = GetComponentInChildren<OutlineHighlighter>();
    }
    public void Interact()
    {
        // If currently available for interaction, check whether Detective Vision is currently highlighting objects.
        // If not, trigger standard dialogue.
        
        // If the objects condition for being highlighted is true, and is also currently being highlighted with Detective Vision:
        if (outline != null && outline.hasBeenHighlighted)
        {
            //Debug.Log("Detective vision enabled, and item is interacted with");
            if (inkJson2 != null) // Null check in case the second INK-file is missing 
            {
                // Passing components of the NPC that we want to control when interacting with another gameObject.
                Animator anim = null;
                TestAIScript aiScript = null;
                if (!string.IsNullOrEmpty(NPCName))
                {
                    anim = GameObject.Find(NPCName).GetComponentInChildren<Animator>();
                    aiScript = GameObject.Find(NPCName).GetComponent<TestAIScript>();
                }
                NewDialogueManager.Instance.EnterDialogue(inkJson2, anim, aiScript);
                gameObject.tag = "Untagged"; // Untags the gameObject, no longer allowing us to highlight it or enter this path again
            }

            if (!pickedUpClue && item != null)
            {
                playerInventory.AddItem(new Item(item));
                if (item2 != null)
                    playerInventory.AddItem(new Item(item2));
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
