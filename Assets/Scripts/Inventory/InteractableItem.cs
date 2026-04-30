using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("References")]
    private InventoryObject playerInventory;
    [SerializeField] ItemObject item;
    [SerializeField] TextAsset inkJson, inkJson2;
    HighlightActivatorIAVersion highlighter;

    void Start()
    {
        playerInventory = PlayerController.Instance.inventory;
        highlighter = GameObject.FindGameObjectWithTag("Player").GetComponent<HighlightActivatorIAVersion>();
    }

    void Update()
    {
    }
    public void Interact()
    {
        if (inkJson != null)
            NewDialogueManager.Instance.EnterDialogue(inkJson, null);

        else if (highlighter.IsHighlighting)
        {
            
            Debug.Log("Detective vision enabled, and item is interacted with");

            if (item != null)
            {
                if (inkJson2 != null)
                    NewDialogueManager.Instance.EnterDialogue(inkJson2, null);
                playerInventory.AddItem(new Item(item));
                Destroy(gameObject);
            }
            //gameObject.tag = "Untagged";
        }
        else Debug.Log("Detective vision NOT enabled, and item is interacted with");
    }
}
