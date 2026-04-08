//using UnityEngine;

//public class PlayerInventoryTEST : MonoBehaviour
//{
//    [Header("Your Inventory")]
//    public InventoryObjectTEST inventory;

//    private void OnTriggerEnter(Collider other)
//    {
//        // Check if the thing we bumped into has an "Item" script attached
//        Item physicalItem = other.GetComponent<Item>();

//        if (physicalItem != null)
//        {
//            // Put the data into the backpack
//            inventory.AddItem(physicalItem.item);

//            // Destroy the 3D cube in the scene so it looks like we picked it up
//            Destroy(other.gameObject);
//        }
//    }

//    // Optional: Clears out your test inventory every time you close the game 
//    // so you can test picking it up fresh every time.
//    private void OnApplicationQuit()
//    {
//        inventory.Container.Clear();
//    }
//}