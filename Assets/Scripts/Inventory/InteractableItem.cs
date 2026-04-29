using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("References")]
    public InventoryObject playerInventory;
    public ItemObject garlicBreathClue;
    [SerializeField] TextAsset inkJson;


    
    //HighlightActivatorIAVersion highlighter;
    

    void Awake()
    {
        
    }

    void Update()
    {
        
    }
    public void Interact()
    {
        if (inkJson != null)
            NewDialogueManager.Instance.EnterDialogue(inkJson, null);

        // Add the item to the inventory
        playerInventory.AddItem(new Item(garlicBreathClue));

        GameObject.Destroy(gameObject);
        //gameObject.tag = "Untagged";
        

    }
}
