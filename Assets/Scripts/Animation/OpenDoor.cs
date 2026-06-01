using UnityEngine;

/// <summary>
/// Simple script for determining if you're allowed to open the door at the back of the gas station. Checks a LIST within INK
/// to see if it contains the item needed to interact with the door.
/// </summary>
public class OpenDoor : MonoBehaviour, IInteractable
{
    Animator animator;
    [SerializeField] string inkTrigger;
    [SerializeField] TextAsset inkJson;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        bool trigger = NewDialogueManager.Instance.InkListContainsItem(inkTrigger, "items");
        if (!trigger)
        {
            NewDialogueManager.Instance.EnterDialogue(inkJson, null, null);
            return;
        }
        // Since dialogue does not trigger when opening the door, I can't pass along the door's animator. I have to set the trigger manually
        animator.SetTrigger("IsOpen");
    }
}
