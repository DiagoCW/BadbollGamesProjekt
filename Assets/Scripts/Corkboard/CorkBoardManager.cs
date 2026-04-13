using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CorkboardManager : MonoBehaviour
{
    public static event System.Action OnCorkboardCompleted;
    
    [Header("UI References")]
    private bool isPuzzleSolved = false;
    [SerializeField] private Button finishButton;
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private TextMeshProUGUI resultText;
    
    [Header("End Scene Camera")]
    [SerializeField] private Camera endSceneCamera;

    [Header("Correct Order")]
    [SerializeField] private int[] correctOrder = { 1, 2, 3, 4 };

    private Slot[] slots;
    private CorkboardTrigger corkboardTrigger;

    private void Awake()
    {
        slots = Object.FindObjectsByType<Slot>(FindObjectsSortMode.None);
        corkboardTrigger = Object.FindAnyObjectByType<CorkboardTrigger>();

        if (finishButton != null)
            finishButton.gameObject.SetActive(false);

        if (resultCanvas != null)
            resultCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isPuzzleSolved) return;

        if (AllSlotsFilled())
        {
            int correctCount = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                DraggableClue clue = slots[i].GetComponentInChildren<DraggableClue>();
                if (clue != null && clue.correctSlotID == slots[i].slotID)
                    correctCount++;
            }

            if (correctCount == slots.Length)
            {
                isPuzzleSolved = true;

                if (finishButton != null) finishButton.gameObject.SetActive(false);

                CalculateAndShowResult();
            }
        }
    }

    private bool AllSlotsFilled()
    {
        return slots.All(s => s.IsOccupied());
    }

    public void OnFinishButtonPressed()
    {
        if (finishButton != null)
        {
            finishButton.gameObject.SetActive(false);
            finishButton.interactable = false;
        }

        CalculateAndShowResult();
    }

    private void CalculateAndShowResult()
    {
        int correctCount = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            DraggableClue clue = slots[i].GetComponentInChildren<DraggableClue>();
            if (clue != null && clue.correctSlotID == slots[i].slotID)
                correctCount++;
        }

        bool won = correctCount == slots.Length;

        if (won)
        {
            Debug.Log("Puzzle Solved! Event triggered");
            OnCorkboardCompleted?.Invoke();
            
        }

        if (corkboardTrigger != null)
            corkboardTrigger.ForceExit();

        if (endSceneCamera != null)
        {
            Camera.main.gameObject.SetActive(false);
            endSceneCamera.gameObject.SetActive(true);
        }

        if (resultCanvas != null)
        {
            resultCanvas.SetActive(true);

            if (resultText != null)
            {
                resultText.text = won
                    ? $"You Win!\n\n{correctCount}/4 clues correct"
                    : $"Game Over\n\n{correctCount}/4 clues correct\n\nBetter luck next time!";
            }
        }
    }
}