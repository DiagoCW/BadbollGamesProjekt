using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    [Header("References")]
    public InventoryObject playerInventory; 
    public ItemObject garlicBreathClue;     

    private bool hasDiscoveredBreath = false;
    HighlightActivatorIAVersion highlighter;

    void Awake()
    {
        highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();
    }

    public void Interact()
    {
        if (!highlighter.IsHighlighting) return;
        if (!hasDiscoveredBreath)
        {
            Debug.Log("Wow, this guy needs a mint. Added Garlic Breath Clue!");

            // Add the item to the inventory
            playerInventory.AddItem(new Item(garlicBreathClue));

            hasDiscoveredBreath = true;
        }

    }
}
