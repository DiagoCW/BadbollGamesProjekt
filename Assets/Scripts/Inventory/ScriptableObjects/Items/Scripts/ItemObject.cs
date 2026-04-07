using UnityEngine;

//-Excluded-
//public enum ItemType
//{
//    Food,
//    Equipment,
//    Default
//}
//-Excluded-

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    //-Excluded-
    //public ItemType type;
    //-Excluded-
    [TextArea(15, 20)]
    public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}