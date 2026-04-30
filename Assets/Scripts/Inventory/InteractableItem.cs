using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("References")]
    private InventoryObject playerInventory;
    public ItemObject item;
    [SerializeField] TextAsset inkJson;


    
    //HighlightActivatorIAVersion highlighter;
    

    void Start()
    {
        playerInventory = PlayerController.Instance.inventory;
    }

    void Update()
    {
        
    }
    public void Interact()
    {
        if (inkJson != null)
            NewDialogueManager.Instance.EnterDialogue(inkJson, null);

        // Add the item to the inventory
        playerInventory.AddItem(new Item(item));

        GameObject.Destroy(gameObject);
        //gameObject.tag = "Untagged";
        

    }
}
