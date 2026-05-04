using UnityEngine;

public class CorkboardTriggerV2 : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private GameInput gameInput;

    //[Header("Spawner")]
    //[SerializeField] private CorkboardSpawnerV2 spawner;

    [Header("Cameras")]
    [SerializeField] private GameObject playerCameraGO;
    [SerializeField] public GameObject corkboardCameraGO;

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";

    //[Header("Optional Behaviour")]
    //[SerializeField] private MonoBehaviour[] scriptsToDisableWhileViewing;

    public bool isViewingCorkboard { get; private set; } = false;
    public bool isPlayerNearBoard { get; private set; } = false;
    private bool firstTimeViewing = true;
    [SerializeField] TextAsset inkJson;


    private void Start()
    {
        if (playerCameraGO == null) Debug.LogError("PlayerCameraGO not assigned!", this);
        if (corkboardCameraGO == null) Debug.LogError("CorkboardCameraGO not assigned!", this);

        SetViewToPlayer();

        if (gameInput != null) 
        {
            gameInput.OnInteractAction += GameInput_OnInteractAction;
            gameInput.OnExitAction += GameInput_OnExitAction;
        }
    }

    private void OnDestroy()
    {
        if (gameInput != null) 
        {
            gameInput.OnInteractAction -= GameInput_OnInteractAction;
            gameInput.OnExitAction -= GameInput_OnExitAction;
        }
    }

    private void Update()
    {
        if (isViewingCorkboard) 
        {
            if (Cursor.lockState != CursorLockMode.Confined || !Cursor.visible) 
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNearBoard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNearBoard = false;
            if (isViewingCorkboard) SwitchToCorkboard(false);
        }
    }

    private void SwitchToCorkboard(bool toCorkboard)
    {
        isViewingCorkboard = toCorkboard;

        if (toCorkboard) 
        {
            if (firstTimeViewing && inkJson != null)
            {
                NewDialogueManager.Instance.EnterDialogue(inkJson, null, null);
                firstTimeViewing = false;
            }
            playerCameraGO.SetActive(false);
            corkboardCameraGO.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else 
        {
            corkboardCameraGO.SetActive(false);
            playerCameraGO.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ForceExit()
    {
        if (isViewingCorkboard)
            SwitchToCorkboard(false);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) 
    {
        if (isPlayerNearBoard && !NewDialogueManager.Instance.dialogueIsPlaying) 
        {
            SwitchToCorkboard(!isViewingCorkboard);
        }
    }

    private void GameInput_OnExitAction(object sender, System.EventArgs e) 
    {
        if (isViewingCorkboard)
        {
            SwitchToCorkboard(false);
        }
    }
    private void SetViewToPlayer()
    {
        playerCameraGO.SetActive(true);
        corkboardCameraGO.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}