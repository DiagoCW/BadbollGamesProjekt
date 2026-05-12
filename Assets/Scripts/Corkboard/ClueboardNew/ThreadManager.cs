using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the creation, routing, and validation of visual threads between SuspectNodes and ClueSlots.
/// </summary>
public class ThreadManager : MonoBehaviour
{
    public static ThreadManager Instance { get; private set; }

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

            // limit connections to 3 by removing the oldest one.
            if (suspectWebs[activeSuspect].Count >= 3)
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

            // Check  the suspect's solution state.
            CheckSuspectSolution(activeSuspect);

            // Reset drawing state variables.
            activeSuspect = null;
            activeLine = null;
            boardTrigger.isDraggingThread = false;
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
        }
    }

    /// <summary>
    /// evaluates the connected clues for a suspect against their clue solving solution
    /// </summary>
    public void CheckSuspectSolution(SuspectNode suspect)
    {
        SuspectSolution solution = caseSolutions.Find(s => s.suspect == suspect);

        // No evaluation if no valid solution configuration exists for this node
        if (solution.suspect == null || solution.requiredClues.Count == 0) return;
        if (!suspectWebs.ContainsKey(suspect)) return;

        Queue<ClueSlot> connectedSlots = suspectWebs[suspect];

        // Collect IDs of clues currently submitted
        List<int> playerSubmittedIDs = new List<int>();
        foreach (ClueSlot slot in connectedSlots)
        {
            if (slot.currentClue != null)
                playerSubmittedIDs.Add(slot.currentClue.itemID);
        }

        // Resolve required ItemObjects to their IDs.
        List<int> requiredIDs = new List<int>();
        foreach (ItemObject item in solution.requiredClues)
        {
            if (item != null) requiredIDs.Add(item.Id);
        }

        // Verification
        string reqString = string.Join(", ", requiredIDs);
        string subString = string.Join(", ", playerSubmittedIDs);
        Debug.Log($"[Validation] {suspect.name} requires IDs: [{reqString}]. Submitted: [{subString}]");

        // check connection count matches the required count
        if (playerSubmittedIDs.Count != requiredIDs.Count)
        {
            Debug.Log($"[Validation] Failed: {suspect.name} connection count mismatch.");
            return;
        }

        // check exact match of submitted clues against required clues
        List<int> tempRequired = new List<int>(requiredIDs);
        foreach (int id in playerSubmittedIDs)
        {
            if (tempRequired.Contains(id))
            {
                tempRequired.Remove(id);
            }
            else
            {
                Debug.Log($"[Validation] Failed: Clue ID {id} is not valid for {suspect.name}.");
                return;
            }
        }

        Debug.Log($"[Validation] Success: {suspect.name} solution verified.");
        CheckEntireBoard();
    }

    /// <summary>
    /// Evaluates all configured suspect solutions to determine if the entire board is solved.
    /// </summary>
    private void CheckEntireBoard()
    {
        int totalSolved = 0;

        foreach (SuspectSolution solution in caseSolutions)
        {
            if (!suspectWebs.ContainsKey(solution.suspect)) continue;

            List<int> playerSubmittedIDs = new List<int>();
            foreach (ClueSlot slot in suspectWebs[solution.suspect])
            {
                if (slot.currentClue != null) playerSubmittedIDs.Add(slot.currentClue.itemID);
            }

            // resolve required ItemObjects to IDs
            List<int> requiredIDs = new List<int>();
            foreach (ItemObject item in solution.requiredClues)
            {
                if (item != null) requiredIDs.Add(item.Id);
            }

            // skip strict validation if the lengths do not match.
            if (playerSubmittedIDs.Count != requiredIDs.Count) continue;

            List<int> tempRequired = new List<int>(requiredIDs);
            bool isCorrect = true;
            foreach (int id in playerSubmittedIDs)
            {
                if (tempRequired.Contains(id)) tempRequired.Remove(id);
                else { isCorrect = false; break; }
            }

            // Increment solved count if all conditions are ther
            if (isCorrect) totalSolved++;
        }

        // Verify if the number of solved suspects matches the total number of configured case solutions.
        if (totalSolved == caseSolutions.Count && caseSolutions.Count > 0)
        {
            Debug.Log("The case is solved");
        }
    }
}