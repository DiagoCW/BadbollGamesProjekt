using UnityEngine;

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
        //bool trigger = animator.GetBool("isOpen");
        //animator.SetBool("isOpen", !trigger);
        animator.SetTrigger("IsOpen");
    }
}
