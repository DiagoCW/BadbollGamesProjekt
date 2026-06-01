using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author: Stefan Cwiek
/// 
/// Hold the database of all suspects and handles putting their photos on the board.
/// </summary>
public class SuspectManager : MonoBehaviour
{
    public static SuspectManager Instance { get; private set; } // Other scripts can easily call this using SuspectManager.Instance
    
    [System.Serializable]
    public struct SuspectData
    {
        public string suspectName;
        public int suspectID;
        public Sprite suspectPhoto;
    }

    [Header("Database")]
    [Tooltip("Assign the photos and IDs for all possible suspects here.")]
    [SerializeField] private List<SuspectData> suspectDatabase;

    [Header("UI References")]
    [Tooltip("Drag your 3 SuspectNode UI elements here.")]
    [SerializeField] private SuspectNode[] suspectNodes;

    private void Awake()
    {
        // Set up the singleton
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);
    }

    /// <summary>
    ///  Looks up a suspect by their ID and puts their photo into their dedicated fixed slot on the board
    /// </summary>
    /// <param name="id">The ID of the suspect we want to show</param>
    public void UnlockSuspect(int id)
    {
        Sprite photoToAssign = null;

        // Find the photo for this ID in our database
        foreach (var data in suspectDatabase)
        {
            if (data.suspectID == id)
            {
                photoToAssign = data.suspectPhoto;
                break;
            }
        }

        // Stop if asked for an ID that doesn't exist
        if (photoToAssign == null)
        {
            Debug.LogWarning($"Suspect ID {id} not found in database!");
            return;
        }

        foreach (SuspectNode node in suspectNodes)
        {
            // Check if the slot belong to the suspect that is getting unlocked Does this slot belong to the suspect we are trying to unlock?
            if (node.suspectID == id)
            {
                // ignore if we already unlocked 
                if (node.isUnlocked) return;

                // Unlock in specific slot
                node.UnlockSuspect(photoToAssign);
               // Debug.Log($"Unlocked Suspect {id}");
                return;
            }
        }

        // Failsafe
        Debug.LogWarning($"No SuspectNode UI element found with ID {id} on the board");
    }
}