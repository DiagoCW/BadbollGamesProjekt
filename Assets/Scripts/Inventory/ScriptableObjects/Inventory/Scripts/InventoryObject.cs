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
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item == _item)
            {
                //-Excluded-
                //Container.Items[i].AddAmount(-amount);
                //-Excluded-
                return;
            }
        }
        Container.Items.Add(new InventorySlot(_item.Id, _item/*, _amount*/));
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    //-Excluded-
    //public int amount;
    //-Excluded-
    public InventorySlot(int _id, Item _item/*, int _amount*/)
    {
        ID = _id;
        item = _item;
        //-Excluded-
        //amount = _amount;
        //-Excluded-
    }

    //public void AddAmount(int value)
    //{
    //    //-Excluded-
    //    //amount += value;
    //    //-Excluded-
    //}
}