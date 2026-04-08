using UnityEngine;

public class InteractableTrashBin : MonoBehaviour, IInteractable
{
    [Header("References")]
    public InventoryObject playerInventory;
    public ItemObject receiptClue;

    private bool hasBeenSearched = false;
    public void Interact()
    {
        if (!hasBeenSearched)
        {
            Debug.Log("You search the trash and find a suspicious receipt!");

            // Add the item to the inventory
            playerInventory.AddItem(new Item(receiptClue));

            hasBeenSearched = true;
        }
        else
        {
            Debug.Log("Just an empty, smelly trash can now.");
        }

    }
}
