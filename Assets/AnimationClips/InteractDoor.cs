using UnityEngine;

public class InteractDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator doorAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        doorAnimator.SetBool("isOpen", !doorAnimator.GetBool("isOpen"));
    }
}
