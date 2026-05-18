using UnityEngine;
using System.Collections.Generic;

/// <summary>
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
    ///  Looks up a suspect by their ID and puts their photo into the first empty frame on the board
    /// </summary>
    /// <param name="id">The ID of the suspect we want to show</param>
    public void UnlockSuspect(int id)
    {
        Sprite photoToAssign = null;

        // Find the photo for this ID in our database
        foreach (var data in suspectDatabase)
        {
            if (data.suspectID == id /*&& NewDialogueManager.Instance.CheckSuspectsList(data.suspectName)*/)
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

        // find an empty node on the board to put the photo in
        foreach (SuspectNode node in suspectNodes)
        {
            // ignore if we already unlocked this specific suspect
            if (node.isUnlocked && node.suspectID == id) return;

            if (!node.isUnlocked)
            {
                node.suspectID = id;
                node.UnlockSuspect(photoToAssign);
                Debug.Log($"Unlocked Suspect {id} on the board");
                return;
            }
        }
    }
}