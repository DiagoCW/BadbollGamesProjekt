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
    public GameObject prefab;
    //-Excluded-
    //public ItemType type;
    //-Excluded-
    [TextArea(15, 20)]
    public string description;
}