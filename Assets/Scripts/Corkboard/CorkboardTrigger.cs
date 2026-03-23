using UnityEngine;

public class CorkboardTrigger : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private GameObject playerCameraGO;         // Drag the MainCamera GameObject
    [SerializeField] private GameObject corkboardCameraGO;      // Drag the Corkboard Camera GameObject

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

        // Force correct starting state
        SetViewToPlayer();

        // Ensure cursor starts locked for normal gameplay
        LockCursor(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isViewingCorkboard)
        {
            Debug.Log($"Player ({other.gameObject.name}) ENTERED trigger → switch to corkboard");
            SwitchToCorkboard(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && isViewingCorkboard)
        {
            Debug.Log($"Player ({other.gameObject.name}) EXITED trigger → back to player");
            SwitchToCorkboard(false);
        }
    }

    private void Update()
    {
        if (isViewingCorkboard && Input.GetKeyDown(exitKey))
        {
            Debug.Log("Escape pressed → force exit corkboard view");
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

            // Force depth priority
            var cbCam = corkboardCameraGO.GetComponent<Camera>();
            if (cbCam != null) cbCam.depth = 10f;

            // Unlock cursor so player can interact with UI (drag clues, click buttons)
            LockCursor(false);

            Debug.Log("Switched TO corkboard view → cursor UNLOCKED for UI interaction");
        }
        else
        {
            corkboardCameraGO.SetActive(false);
            playerCameraGO.SetActive(true);

            var playerCam = playerCameraGO.GetComponent<Camera>();
            if (playerCam != null) playerCam.depth = -1f;

            // Re-lock cursor for normal movement
            LockCursor(true);

            Debug.Log("Switched BACK to player view → cursor RE-LOCKED");
        }

        // Disable script while active
        foreach (var script in scriptsToDisableWhileViewing)
        {
            if (script != null)
                script.enabled = !toCorkboard;
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