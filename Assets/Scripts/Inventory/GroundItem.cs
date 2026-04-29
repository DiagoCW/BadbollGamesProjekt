using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    public ItemObject item;

    public void Interact()
    {
        // Implement interaction logic here, e.g., adding the item to the player's inventory
        Debug.Log($"Interacted with {item.name}");
    }
}