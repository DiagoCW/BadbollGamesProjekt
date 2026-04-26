using UnityEngine;

public class DebugSlotOccupancy : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // Press O to check
        {
            SlotV2[] slots = Object.FindObjectsByType<SlotV2>(FindObjectsSortMode.None);
            foreach (var s in slots)
            {
                Debug.Log($"Slot {s.slotID} → Occupied: {s.IsOccupied()}");
            }
        }
    }
}