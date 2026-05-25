using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEditor.Search;

/// <summary>
/// Manages the creation, routing, and validation of visual threads between SuspectNodes and ClueSlots.
/// </summary>
public class ThreadManager : MonoBehaviour // Author - Stefan Cwiek
{
    public static ThreadManager Instance { get; private set; }

    // Other scripts can listen to this to trigger whatever is to be triggered
    public static event Action OnCaseSolved;
    public static event Action OnCaseFailed;

    // This tells the submit button to check if it should change color
    public static event Action OnThreadsChanged;

    [Header("Case Solutions")]
    [Tooltip("Defines the required clue combinations for each suspect.")]
    public List<SuspectSolution> caseSolutions = new List<SuspectSolution>();

    /// <summary>
    /// Insertr the required solution data for a specific suspect.
    /// </summary>
    [System.Serializable]
    public struct SuspectSolution
    {
        public SuspectNode suspect;

        [Tooltip("The list of ItemObjects required to solve this suspect's case.")]
        public List<ItemObject> requiredClues;
    }

    [Header("References")]
    public GameObject threadPrefab;
    public RectTransform threadContainer;
    public ClueboardTrigger boardTrigger;

    // Active state tracking
    private SuspectNode activeSuspect;
    private ThreadLine activeLine;

    // Data structures for connection tracking and validation
    private Dictionary<ClueSlot, ThreadLine> connections = new Dictionary<ClueSlot, ThreadLine>();
    private Dictionary<SuspectNode, Queue<ClueSlot>> suspectWebs = new Dictionary<SuspectNode, Queue<ClueSlot>>();

    private void Awake()
    {
        // Singleton setup
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Retrieves the current number of clues connected to a specified suspect
    /// </summary>
    /// <param name="suspect">The target suspect node</param>
    /// <returns>The integer count of connected clue slots</returns>
    public int GetClueCountForSuspect(SuspectNode suspect)
    {
        if (suspectWebs.ContainsKey(suspect))
            return suspectWebs[suspect].Count;
        return 0;
    }

    private void Update()
    {
        // Updates the active thread line to follow the cursor position during draw
        if (activeSuspect != null && activeLine != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                threadContainer,
                Input.mousePosition,
                threadContainer.GetComponentInParent<Canvas>().worldCamera,
                out Vector2 localMousePos);

            // converts the suspect world position into the thread container local space
            Vector3 startPos = threadContainer.InverseTransformPoint(activeSuspect.transform.position);

            activeLine.UpdateLine(startPos, localMousePos);

            // cancels the drawing operation on ríght click or inventory key.
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Tab))
            {
                CancelDrawing();
            }
        }
    }

    /// <summary>
    /// Initiates the thread drawing process from a specified suspect node
    /// </summary>
    public void StartDrawing(SuspectNode suspect)
    {
        // dont do initialization if a draw operation is already in progress
        if (activeSuspect != null) return;

        activeSuspect = suspect;
        boardTrigger.isDraggingThread = true;

        GameObject line = Instantiate(threadPrefab, threadContainer, false);
        activeLine = line.GetComponent<ThreadLine>();
    }

    /// <summary>
    /// Attempts to connect the active thread to the provided clue slot or disconnects an existing thread.
    /// </summary>
    public void TryConnect(ClueSlot slot)
    {
        // disconnection if no drawing operation is active
        if (activeSuspect == null && connections.ContainsKey(slot))
        {
            RemoveConnection(slot);
            return;
        }

        // connection request to an occupied slot
        if (activeSuspect != null && slot.IsOccupied)
        {
            // Remove existing connection on the target slot if present
            if (connections.ContainsKey(slot))
            {
                RemoveConnection(slot);
            }

            // Initialize the suspect connection queue if necessary.
            if (!suspectWebs.ContainsKey(activeSuspect))
            {
                suspectWebs[activeSuspect] = new Queue<ClueSlot>();
            }

            // Trying something new. Dynamically find how many clues this specific case needs
            int maxConnectionsAllowed = 3;
            SuspectSolution solution = caseSolutions.Find(s => s.suspect == activeSuspect);
            if (solution.suspect != null) maxConnectionsAllowed = solution.requiredClues.Count;

            // limit connections to 3 by removing the oldest one.
            if (suspectWebs[activeSuspect].Count >= maxConnectionsAllowed)
            {
                ClueSlot oldestSlot = suspectWebs[activeSuspect].Dequeue();
                if (connections.ContainsKey(oldestSlot))
                {
                    Destroy(connections[oldestSlot].gameObject);
                    connections.Remove(oldestSlot);
                }
            }

            Vector3 startPos = threadContainer.InverseTransformPoint(activeSuspect.transform.position);
            Vector3 endPos = threadContainer.InverseTransformPoint(slot.transform.position);

            // update line rendering to snap between the suspect and the slot
            activeLine.UpdateLine(startPos, endPos);

            // Register the new connection in tracking dictionaries
            connections.Add(slot, activeLine);
            suspectWebs[activeSuspect].Enqueue(slot);

            // Reset drawing state variables.
            activeSuspect = null;
            activeLine = null;
            boardTrigger.isDraggingThread = false;

            OnThreadsChanged?.Invoke(); // Tell the ui button that a thread was attached
        }
    }

    /// <summary>
    /// cancels the current thread drawing operation and cleans up temporary objects
    /// </summary>
    public void CancelDrawing()
    {
        if (activeLine != null) Destroy(activeLine.gameObject);
        activeSuspect = null;
        activeLine = null;
        boardTrigger.isDraggingThread = false;
    }

    /// <summary>
    /// Removes a specific thread connection associated with a clue slot
    /// </summary>
    public void RemoveConnection(ClueSlot slot)
    {
        if (connections.ContainsKey(slot))
        {
            Destroy(connections[slot].gameObject);
            connections.Remove(slot);

            // Remove the slot from the suspect queue 
            foreach (var suspectQueue in suspectWebs.Values)
            {
                var list = new List<ClueSlot>(suspectQueue);
                if (list.Remove(slot))
                {
                    suspectQueue.Clear();
                    foreach (var s in list) suspectQueue.Enqueue(s);
                    break;
                }
            }

            OnThreadsChanged?.Invoke(); // Tell UI button that a thread was removed
        }
    }

    /// <summary>
    /// Checks if any suspect on the board 3 clues attached to them. If so, the submit button will turn green
    /// </summary>
    public bool IsBoardReadyToSubmit() 
    {
        foreach (var web in suspectWebs) 
        {
            SuspectNode suspect = web.Key;
            Queue<ClueSlot> connectedSlots = web.Value;

            SuspectSolution solution = caseSolutions.Find(s => s.suspect == suspect);
            if (solution.suspect != null && connectedSlots.Count == solution.requiredClues.Count) 
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Called when the player clicks the submit case button. Evaluates only the suspects that have exactly 3 clues.
    /// </summary>
    public void SubmitCase() 
    {
        if (!IsBoardReadyToSubmit()) return;

        bool solvedCorrectly = false;

        foreach (var kvp in suspectWebs) 
        {
            SuspectNode suspect = kvp.Key;
            Queue<ClueSlot> connectedSlots = kvp.Value;

            // If the suspect doesn't have 3 clues, ignore them
            if (connectedSlots.Count != 3) continue;

            // Check if this suspect has a solution in the database
            SuspectSolution solution = caseSolutions.Find(s => s.suspect == suspect);
            if (solution.suspect == null || solution.requiredClues.Count == 0) continue;

            if (connectedSlots.Count != solution.requiredClues.Count) continue; // Removes the earlier hardcoded 3, now relies on the solution count instead

            // Gather the IDs of the clues the player attached
            List<int> playerSubmittedIDs = new List<int>();
            foreach (ClueSlot slot in connectedSlots) 
            {
                if (slot.currentClue != null) playerSubmittedIDs.Add(slot.currentClue.itemID);
            }

            // gather the IDs of the clues required to prove the suspect guilty
            List<int> requiredIDs = new List<int>();
            foreach (ItemObject item in solution.requiredClues) 
            {
                if (item != null) requiredIDs.Add(item.Id);
            }

            // Check if they match
            if (playerSubmittedIDs.Count == requiredIDs.Count) 
            {
                List<int> tempRequired = new List<int>(requiredIDs);
                bool isCorrect = true;

                foreach (int id in playerSubmittedIDs) 
                {
                    if (tempRequired.Contains(id)) tempRequired.Remove(id);
                    else { isCorrect = false; break; }
                }

                if (isCorrect) 
                {
                    solvedCorrectly = true;
                    break;
                }
            }
        }

        // Trigger end game
        if (solvedCorrectly) 
        {
            Debug.Log("The case is solved");
            OnCaseSolved?.Invoke(); // Tells other scripts that the player solved the case
        }
        else 
        {
            Debug.Log("The clues dont add up, sorry bro I think you failed");
            OnCaseFailed?.Invoke(); // Tells other scripts that the player failed the case
        }
    }
}