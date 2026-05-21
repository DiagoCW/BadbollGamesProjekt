using UnityEngine;
using Ink.Runtime;

/// <summary>
/// Makes the car pop its hood when triggered from the dialogue in the tutorial scene
/// </summary>
public class CarHoodAnimator : MonoBehaviour // Author - Stefan Cwiek
{
    [Header("References")]
    [Tooltip("The animator component on this object")]
    [SerializeField] private Animator hoodAnimator;
    
    private bool isOpen = false;

    private void Update()
    {

        if (NewDialogueManager.Instance != null && NewDialogueManager.Instance.dialogueVariables != null) 
        {
            if (NewDialogueManager.Instance.dialogueVariables.variables.TryGetValue("isHoodOpen", out Ink.Runtime.Object inkValue)) // Check for the isHoodOpen variable in Ink
            {
                bool inkSaysOpen = (inkValue.ToString() == "true" || inkValue.ToString() == "True"); // Check if the ink dialogue says that the hood should be open (true) or closed (false)

                if (inkSaysOpen && !isOpen) // if ink says open but the hood is closed, open it
                {
                    isOpen = true;
                    hoodAnimator.SetTrigger("OpenHood");
                }
                else if (!inkSaysOpen && isOpen) // if ink says closed but the hood is open, close it
                {
                    isOpen = false;
                    hoodAnimator.SetTrigger("CloseHood");
                }
            }
        }
    }
}
