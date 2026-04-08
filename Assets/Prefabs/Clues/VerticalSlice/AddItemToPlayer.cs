using UnityEngine;

public class AddItemToPlayer : MonoBehaviour
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

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        inventory.Save();
    //    }
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        inventory.Load();
    //    }
    //}

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[2];
    }
}
