//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//using UnityEditor;

//[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/TEST Inventory")]
//public class InventoryObjectTEST : ScriptableObject, ISerializationCallbackReceiver
//{
//    public string savePath;
//    private ItemDatabaseObject databaseTEST;
//    public List<InventorySlot> Container = new List<InventorySlot>(); 

//    private void OnEnable()
//    {
//        databaseTEST = Resources.Load<ItemDatabaseObject>("DatabaseTest");

//        if (databaseTEST == null) 
//        {
//            Debug.LogError("Could not find the Database! Make sure it is inside a folder named 'Resources' and is named exactly 'Database'.");
//        }
//    }

//    public void AddItem(ItemObject _item/*, int _amount*/)
//    {
//        for (int i = 0; i < Container.Count; i++)
//        {
//            if (Container[i].item == _item)
//            {
//                //-Excluded-
//                //Container[i].AddAmount(-amount);
//                //-Excluded-
//                return;
//            }
//        }
//        Container.Add(new InventorySlot(databaseTEST.GetId[_item], _item/*, _amount*/));
//    }

//    public void Save()
//    {
//        string saveData = JsonUtility.ToJson(this, true);
//        BinaryFormatter bf = new BinaryFormatter();
//        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
//        bf.Serialize(file, saveData);
//        file.Close();
//    }

//    public void Load()
//    {
//        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
//        {
//            BinaryFormatter bf = new BinaryFormatter();
//            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
//            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
//            file.Close();
//        }
//    }

//    public void OnAfterDeserialize()
//    {
//        for (int i = 0; i < Container.Count; i++)
//        {
//            Container[i].item = databaseTEST.GetItem[Container[i].ID];
//        }
//    }

//    public void OnBeforeSerialize()
//    {

//    }

//    [System.Serializable]
//    public class InventorySlot
//    {
//        public int ID;
//        public ItemObject item;
//        //-Excluded-
//        //public int amount;
//        //-Excluded-
//        public InventorySlot(int _id, ItemObject _item/*, int _amount*/)
//        {
//            ID = _id;
//            item = _item;
//            //-Excluded-
//            //amount = _amount;
//            //-Excluded-
//        }

//        public void AddAmount(int value)
//        {
//            //-Excluded-
//            //amount += value;
//            //-Excluded-
//        }
//    }
//}