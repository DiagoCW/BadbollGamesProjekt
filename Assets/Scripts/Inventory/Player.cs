using UnityEngine;

public class Player : MonoBehaviour
{
    // this script used to be asigned to the player before it's functionality
    // was moved to the PlayerController script, Parts of this code are still
    // used in the player controller script and the player interact script. -Hugo
    public InventoryObject inventory;

    // Initially the player picked up items by walking over them, this was
    // later changed to happen on pressing E.
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item));
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        // Don't include for now, Only if we add a save or load feature.
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    inventory.Save();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    inventory.Load();
        //}
    }

    // This is the only part of the code still in use, it clears the
    // inventory when the application is closed.
    private void OnApplicationQuit()
    {
        inventory.Clear();
        inventory.Container.Items = new InventorySlot[12];
    }
}
