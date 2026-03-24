using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayClues : MonoBehaviour
{
    public InventoryObject inventory;

    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEM;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        createDisplay();
    }

    void Update()
    {
        // UpdateDisplay();
    }
    public void createDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            // obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name;
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN), Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN), 0);
    }
}
