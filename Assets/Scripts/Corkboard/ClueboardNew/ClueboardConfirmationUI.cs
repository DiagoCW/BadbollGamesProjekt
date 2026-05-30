using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The UI panel for the clueboard whether you accept the solution or not
/// </summary>
public class ClueboardConfirmationUI : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private ClueboardTrigger boardTrigger;

    [Header("Dialogue")]
    [Tooltip("The text that plays after confirming")]
    [SerializeField] private TextAsset confirmationDialogue;

    private void Awake()
    {
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
        confirmationPanel.SetActive(false);
    }

    public void ShowPanel() 
    {
        confirmationPanel.SetActive(true);
    }

    private void OnNoClicked() 
    {
        confirmationPanel.SetActive(false);
    }

    private void OnYesClicked() 
    {
        confirmationPanel.SetActive(false);

        if (ThreadManager.Instance != null)
        {
            ThreadManager.Instance.SetBoardLockState(true);
        }

        // lock the board
        if (ThreadManager.Instance != null) 
        {
            ThreadManager.Instance.isLocked = true;
        }

        // close the board UI
        if (boardTrigger != null && boardTrigger.isViewingBoard) 
        {
            boardTrigger.ToggleBoard(false);
        }

        // play the dialogue 
        if (confirmationDialogue != null && NewDialogueManager.Instance != null) 
        {
            NewDialogueManager.Instance.EnterDialogue(confirmationDialogue, null, null);
        }
    }
}
