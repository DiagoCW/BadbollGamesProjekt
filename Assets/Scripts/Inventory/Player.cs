using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

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
        // Don't include for now
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    inventory.Save();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    inventory.Load();
        //}
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        inventory.Container.Items = new InventorySlot[12];
    }
}
