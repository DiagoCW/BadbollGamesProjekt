using UnityEngine;
using System.Collections.Generic;

public class CorkboardSpawnerV2 : MonoBehaviour
{
    [Header("References")]
    public InventoryObject playerInventory;
    public Transform clueTray;

    [System.Serializable]
    public struct ClueMap
    {
        public ItemObject inventoryItem;
        public GameObject cluePrefab;
    }

    [Header("Clue Database")]
    public List<ClueMap> clueDatabase;

    private List<int> spawnedItemIDs = new List<int>();

    public void SpawnCollectedClues()
    {

        for (int i = 0; i < playerInventory.Container.Items.Length; i++)
        {
            InventorySlot slot = playerInventory.Container.Items[i];

            if (slot.ID >= 0 && slot.item != null)
            {
                if (spawnedItemIDs.Contains(slot.item.Id))
                {
                    continue;
                }

                foreach (ClueMap map in clueDatabase)
                {
                    if (map.inventoryItem.Id == slot.item.Id)
                    {
                        GameObject newClue = Instantiate(map.cluePrefab, clueTray, false);

                        RectTransform rt = newClue.GetComponent<RectTransform>();
                        rt.localScale = Vector3.one;
                        rt.localEulerAngles = Vector3.zero;

                        spawnedItemIDs.Add(slot.item.Id);

                        break;
                    }
                }
            }
        }
    }

    public void SpawnSingleClue(int itemID) 
    {
        foreach (ClueMap map in clueDatabase) 
        {
            if (map.inventoryItem.Id == itemID) 
            {
                GameObject newClue = Instantiate(map.cluePrefab, clueTray, false);

                RectTransform rt = newClue.GetComponent<RectTransform>();
                rt.localScale = Vector3.one;
                rt.localEulerAngles = Vector3.zero;

                spawnedItemIDs.Add(itemID);

                break;
            }
        
        }
    }
}