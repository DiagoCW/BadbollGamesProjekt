using UnityEngine;
using Ink.Runtime;

/// <summary>
/// Makes the car open the door when triggered from the dialogue in the tutorial scene
/// </summary>
public class CarDoorAnimator : MonoBehaviour, IInteractable // Author - Stefan Cwiek
{
    [Header("References")]
    [Tooltip("The animator component containing the CarOpenDoor and close triggers")]
    [SerializeField] private Animator doorAnimator;
    
    public bool isOpen { get; private set; } = false;

    private float lastInteractTime = 0f;
    private float cooldownDuration = 0.5f;

    public void Interact() 
    {
        if (Time.time - lastInteractTime < cooldownDuration) return;

        lastInteractTime = Time.time;

        isOpen = !isOpen;

        if (isOpen) 
        {
            doorAnimator.SetTrigger("OpenDoor");
            Debug.Log("Door Interacted!");
        }
        else 
        {
            doorAnimator.SetTrigger("CloseDoor");
            Debug.Log("Door Interacted!");
        }
    }
}
