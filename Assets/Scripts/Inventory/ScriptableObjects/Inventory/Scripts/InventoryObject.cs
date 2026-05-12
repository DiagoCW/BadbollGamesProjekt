using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;

    public void AddItem(Item _item/*, int _amount*/)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                //-Excluded-
                //Container.Items[i].AddAmount(-amount);
                //-Excluded-
                return;
            }
        }
        SetEmptySlot(_item);
    }
    public InventorySlot SetEmptySlot(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID == -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item);
                return Container.Items[i];
            }
        }
        // set up funktionality for when inventory is full
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item);
        item2.UpdateSlot(item1.ID, item1.item);
        item1.UpdateSlot(temp.ID, temp.item);
    }

    // "Maybe we should add this in some other fashion, if you drag an item off the panel it's removed."
    //public void RemoveItem(InventorySlot item)
    //{
    //    for (int i = 0; i < Container.Items.Length; i++)
    //    {
    //        if (Container.Items[i].items == _item)
    //        {
    //            Container.Items[i].UpdateSlot(-1, null);
    //        }
    //    }
    //}

    // Don't include for now
    //[ContextMenu("Save")]
    //public void Save()
    //{
    //    string saveData = JsonUtility.ToJson(this, true);
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
    //    bf.Serialize(file, saveData);
    //    file.Close();
    //}

    //[ContextMenu("Load")]
    //public void Load()
    //{
    //    if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
    //        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
    //        file.Close();
    //    }
    //}

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }

    #if UNITY_EDITOR
    private void OnDisable()
    {
        // Clear inventory when exiting play mode in the Editor
        Clear();
    }
    #endif
}

[System.Serializable]
public class Inventory
{
    //public List<InventorySlot> Items = new List<InventorySlot>();
    public InventorySlot[] Items = new InventorySlot[12];
}

[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public Item item;
    //-Excluded-
    //public int amount;
    //-Excluded-
    public InventorySlot()
    {
        ID = -1;
        item = null;
    }
    public InventorySlot(int _id, Item _item/*, int _amount*/)
    {
        ID = _id;
        item = _item;
        //-Excluded-
        //amount = _amount;
        //-Excluded-
    }

    public void UpdateSlot(int _id, Item _item)
    {
        ID = _id;
        item = _item;
    }

    //public void AddAmount(int value)
    //{
    //    //-Excluded-
    //    //amount += value;
    //    //-Excluded-
    //}
}