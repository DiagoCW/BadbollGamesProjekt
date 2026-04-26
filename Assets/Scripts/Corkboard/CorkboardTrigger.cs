using UnityEngine;

public class CorkboardTrigger : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private CorkboardSpawner spawner;

    [Header("Cameras")]
    [SerializeField] private GameObject playerCameraGO;
    [SerializeField] public GameObject corkboardCameraGO;   // ← Made public for CorkboardManagerV2

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private KeyCode exitKey = KeyCode.Escape;

    [Header("Cursor Behaviour")]
    [SerializeField] private bool useConfinedCursor = true;

    [Header("Optional Behaviour")]
    [SerializeField] private MonoBehaviour[] scriptsToDisableWhileViewing;

    private bool isViewingCorkboard = false;

    private void Awake()
    {
        if (playerCameraGO == null) Debug.LogError("PlayerCameraGO not assigned!", this);
        if (corkboardCameraGO == null) Debug.LogError("CorkboardCameraGO not assigned!", this);
        SetViewToPlayer();
        LockCursor(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isViewingCorkboard)
        {
            SwitchToCorkboard(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && isViewingCorkboard)
        {
            SwitchToCorkboard(false);
        }
    }

    private void Update()
    {
        if (isViewingCorkboard && Input.GetKeyDown(exitKey))
        {
            SwitchToCorkboard(false);
        }
    }

    private void SwitchToCorkboard(bool toCorkboard)
    {
        if (toCorkboard == isViewingCorkboard) return;

        isViewingCorkboard = toCorkboard;

        if (toCorkboard)
        {
            playerCameraGO.SetActive(false);
            corkboardCameraGO.SetActive(true);
            LockCursor(false);
            if (spawner != null) spawner.SpawnCollectedClues();
        }
        else
        {
            corkboardCameraGO.SetActive(false);
            playerCameraGO.SetActive(true);
            LockCursor(true);
        }

        foreach (var script in scriptsToDisableWhileViewing)
        {
            if (script != null) script.enabled = !toCorkboard;
        }
    }

    private void SetViewToPlayer()
    {
        playerCameraGO.SetActive(true);
        corkboardCameraGO.SetActive(false);
        isViewingCorkboard = false;
        LockCursor(true);
    }

    private void LockCursor(bool shouldLock)
    {
        if (shouldLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = useConfinedCursor ? CursorLockMode.Confined : CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ForceExit()
    {
        if (isViewingCorkboard)
            SwitchToCorkboard(false);
    }
}