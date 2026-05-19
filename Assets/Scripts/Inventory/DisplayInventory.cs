using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    // This script is responsible for displaying the inventory UI, handling dragging and dropping of items,
    // and showing tooltips when hovering over items. It interacts with the InventoryObject to get the
    // current state of the inventory and updates the UI accordingly. -Hugo

    public MouseItem mouseItem = new MouseItem();

    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public int X_START; // The starting X position for the first inventory slot
    public int Y_START; // The starting Y position for the first inventory slot
    public int X_SPACE_BETWEEN_ITEM; // The horizontal space between inventory slots
    public int NUMBER_OF_COLUMN; // The number of columns in the inventory grid
    public int Y_SPACE_BETWEEN_ITEM; // The vertical space between inventory slots after a new row is made

    // Tooltip related variables
    [Header("Hover Tooltip")]
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipNameText;
    [SerializeField] private TextMeshProUGUI tooltipDescriptionText;
    [SerializeField] private Vector2 tooltipOffset = new Vector2(10, -10);

    // Inventory slot dictionary
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    private GameObject currentHoveredSlot;

    private void Awake()
    {
        // Initialize inventory slots
        CreateSlots();

        // Hide tooltip initially
        tooltipPanel.SetActive(false);
    }

    void Start() { }

    void Update()
    {
        // Only updates the inventory display if the inventory is open
        if (PlayerController.Instance.IsInventoryOpen)
            UpdateSlots();
    }

    // When the life cycle of the inventory ends, call null on everything to prevent floating items glitch -Hugo (After test day 1)
    private void OnDisable()
    {
        if (mouseItem.obj != null)
        {
            Destroy(mouseItem.obj);
            mouseItem.obj = null;
            mouseItem.item = null;
        }

        // Hide tooltip when inventory closes
        tooltipPanel.SetActive(false);
        currentHoveredSlot = null;
    }

    // Detta breakar inventory mellan scenbyten! Av någon anledning kan man ej kalla inventory.Clear() // Oj tack för att du hittade detta :)
    //private void OnDestroy()
    //{
    //    // Clear inventory when this display is destroyed (e.g., scene change)
    //    if (inventory != null)
    //    {
    //        inventory.Clear();
    //    }
    //}

    // Updates the inventory based on inventories current state.
    // Checks each slot in itemsDisplayed dictionary and updates the UI element accordingly.
    // If the slot has an item (ID >= 0), sets the sprite to the item's UI display and makes it visible.
    // If the slot is empty (ID < 0), it clears the sprite and makes it invisible. -Hugo
    public void UpdateSlots()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }

        }

    }

    // Creates the inventory slots based on the inventory's container items at launch.
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    // Helper method to add event triggers to inventory slot game objects.
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    
    // Hovers over a game object
    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            mouseItem.hoverItem = itemsDisplayed[obj];
            
            // Show tooltip if slot has an item
            if (mouseItem.hoverItem.ID >= 0)
            {
                currentHoveredSlot = obj;
                ShowTooltip(mouseItem.hoverItem, obj);
            }
        }

    }
    
    // Doesn't hover over a game object lol
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
        currentHoveredSlot = null;
        
        // Hide tooltip
        HideTooltip();
    }
    
    // Apparent name but when you drag an object
    public void OnDragStart(GameObject obj)
    {
        // Hide tooltip when starting to drag
        HideTooltip();
        currentHoveredSlot = null;
        
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];
    }
    
    // Also obvious name but when you stop dragging an item
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else
        {
            // NEW DEBUG LOGGING: Let's see exactly what the mouse is hitting!
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            Debug.Log($"Dropped item. Mouse hit {results.Count} UI objects.");
            foreach (var res in results)
            {
                Debug.Log($"Hit Object: {res.gameObject.name} | Tag: {res.gameObject.tag}");
            }

            if (IsPointerOverClueboard())
            {
                int itemToDropID = itemsDisplayed[obj].ID;
                
                ClueboardSpawner spawner = FindFirstObjectByType<ClueboardSpawner>();
                if (spawner != null)
                {
                    spawner.SpawnClue(itemToDropID);
                    itemsDisplayed[obj].UpdateSlot(-1, null);
                }
            }
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    
    // When you are CURRENTLY dragging an item
    public void OnDrag(GameObject obj)
    {
        if(mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }

    // Check if the mouse is over an object tagged as "Clueboard".
    private bool IsPointerOverClueboard() 
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Clueboard")) return true;
        }
        return false;
    }

    // Shows the tooltip for the given inventory slot and its corresponding UI object.
    private void ShowTooltip(InventorySlot slot, GameObject slotObject)
    {
        if (tooltipPanel == null) return;
        
        // Get the ItemObject from the database
        ItemObject itemObject = inventory.database.GetItem[slot.ID];
        
        // Set the name (from the scriptable object's name field)
        if (tooltipNameText != null)
        {
            tooltipNameText.text = itemObject.name;
        }
        
        // Set the description from the ItemObject
        if (tooltipDescriptionText != null)
        {
            tooltipDescriptionText.text = itemObject.description;
        }
        
        // Position the tooltip relative to the hovered slot
        RectTransform slotRect = slotObject.GetComponent<RectTransform>();
        if (slotRect != null)
        {
            tooltipPanel.transform.position = slotRect.position + (Vector3)tooltipOffset;
        }
        
        tooltipPanel.SetActive(true);
    }

    // Hides the tooltip panel.
    private void HideTooltip()
    {
        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }
}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}