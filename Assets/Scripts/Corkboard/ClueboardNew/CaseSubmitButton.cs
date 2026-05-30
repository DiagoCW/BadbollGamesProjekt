using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the "Submit Case" button. Listens to the ThreadManager to change color when the board has at least one suspect with 3 clues attached
/// </summary>
[RequireComponent(typeof(Button), typeof(Image))]
public class CaseSubmitButton : MonoBehaviour
{
    [Header("Confirmation UI")]
    [SerializeField] private ClueboardConfirmationUI confirmationUI;

    [Header("Colors")]
    [Tooltip("The color of the button when no suspect has enough clues yet.")]
    public Color notReadyColor = new Color(0.8f, 0.2f, 0.2f, 1f); // Red

    [Tooltip("The color of the button when a suspect is ready to be evaluated")]
    public Color readyColor = new Color(0.2f, 0.8f, 0.2f, 1f); // Green

    private Button buttonComponent;
    private Image buttonImage;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // Auto link the button to method
        buttonComponent.onClick.AddListener(OnButtonClicked);
    }

    private void Start()
    {
        UpdateButtonVisuals();
    }

    private void OnEnable()
    {
        // Subscibe to the event from ThreadManager
        // Every time a thread is added or removed the button change its color
        ThreadManager.OnThreadsChanged += UpdateButtonVisuals;
    }

    private void OnDisable()
    {
        // Unsub when disabled to prevent memory leak and stuff
        ThreadManager.OnThreadsChanged -= UpdateButtonVisuals;
    }

/// <summary>
/// Check the board state and update the buttons color and interactability
/// </summary>
   private void UpdateButtonVisuals() 
    {
        if (ThreadManager.Instance != null && ThreadManager.Instance.isLocked) 
        {
            buttonComponent.interactable = false;
            return;
        }

        if (ThreadManager.Instance != null && ThreadManager.Instance.IsBoardReadyToSubmit()) 
        {
            buttonImage.color = readyColor;

            // unlock the button
            buttonComponent.interactable = true;
        }
        else 
        {
            buttonImage.color = notReadyColor;

            // Lock the button 
            buttonComponent.interactable = false;
        }
   }
    
    // Tell the threadmanager to grade the board when button clicked
    private void OnButtonClicked() 
    {
        if (confirmationUI != null) 
        {
            confirmationUI.ShowPanel();
        }
    }
}
