using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Spawns the physical photo of the clues onto the board. 
/// </summary>
public class ClueboardSpawner : MonoBehaviour
{
    public static ClueboardSpawner Instance { get; private set; } // Other scripts can easily call this using ClueboardSpawner.Instance

    [System.Serializable]
    public struct ClueData
    {
        public int inventoryItemID;
        public Sprite clueSprite;   // The photo of the clue to show on the board
    }

    [Header("References")]
    [Tooltip("The Prefab of the Clue Node")]
    [SerializeField] private GameObject clueNodePrefab;
    
    [Tooltip("The empty object where the clues should spawn")]
    [SerializeField] private Transform centerSpawnTray;

    [Header("Database")]
    [Tooltip("Match the inventory item IDs with the correct photo sprite here")]
    public List<ClueData> clueDatabase;

    // Keeps track of what is currently on the board so duplicates doesn't spawn
    public List<int> spawnedItemIDs = new List<int>();

    public void Awake() 
    {
        // Singleton set up
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Looks up an item by its ID, creates a photo for it, and drops it in the center
    /// </summary>
    /// <param name="itemID">The inventory ID of the item being placed on the board</param>
    public void SpawnClue(int itemID)
    {
        // Stop if this clue is already somewhere on the board
        if (spawnedItemIDs.Contains(itemID))
        {
            Debug.Log($"Clue {itemID} is already on the board");
            return;
        }

        // Search the database to find the photo for the specific item ID 
        Sprite photoToAssign = null;
        foreach (var data in clueDatabase)
        {
            if (data.inventoryItemID == itemID)
            {
                photoToAssign = data.clueSprite;
                break;
            }
        }

        // Stop if asked for an item that doesn't have a photo in the database
        if (photoToAssign == null)
        {
            Debug.LogWarning($"No sprite found for item ID {itemID}!");
            return;
        }

        // Create the photo object and put it in the tray
        GameObject newClue = Instantiate(clueNodePrefab, centerSpawnTray, false);

        // force the new photo to sit in the middle if the tray. Keep its normal size
        RectTransform rt = newClue.GetComponent<RectTransform>();
        rt.localPosition = Vector3.zero;
        rt.localScale = Vector3.one;
        rt.localRotation = Quaternion.identity;

        // Give the new photo its ID and picture
        ClueNode nodeScript = newClue.GetComponent<ClueNode>();
        nodeScript.Setup(itemID, photoToAssign);

        // Add it to the list to keep track
        spawnedItemIDs.Add(itemID);
        Debug.Log($"Successfully spawned clue {itemID} on the board.");
    }
}