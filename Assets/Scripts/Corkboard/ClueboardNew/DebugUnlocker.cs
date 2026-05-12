using UnityEngine;

public class DebugUnlocker : MonoBehaviour
{
    [Header("Which suspect does this unlock?")]
    public int suspectID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SuspectManager manager = FindFirstObjectByType<SuspectManager>();
            if (manager != null)
            {
                manager.UnlockSuspect(suspectID);

                Destroy(gameObject);
            }
        }
    }
}