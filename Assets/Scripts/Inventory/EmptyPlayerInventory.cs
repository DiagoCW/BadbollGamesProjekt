using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<ClueItem>();
        if (item)
        {
            inventory.AddItem(item.item);
            Destroy(other.gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
