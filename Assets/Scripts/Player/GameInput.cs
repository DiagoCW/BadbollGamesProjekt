using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJumpAction;
    public event EventHandler OnHighlightAction;
    public event EventHandler OnInventoryAction;
    public event EventHandler OnInteractAction;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();

        // subscribe to input events
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Highlight.performed += Highlight_performed;
        inputActions.Player.Inventory.performed += Inventory_performed;
        inputActions.Player.Interact.performed += Interact_performed;
    }

    private void OnDestroy()
    {
        // unsubscribe from input events to prevent memory leaks
        inputActions.Player.Jump.performed -= Jump_performed;
        inputActions.Player.Highlight.performed -= Highlight_performed;
        inputActions.Player.Inventory.performed -= Inventory_performed;
        inputActions.Player.Interact.performed -= Interact_performed;
        inputActions.Dispose();
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }
    
    private void Highlight_performed(InputAction.CallbackContext obj)
    {
        OnHighlightAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return inputActions.Player.Move.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetLookVector() 
    {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }

    public void Inventory_performed(InputAction.CallbackContext obj) 
    {
        OnInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    public void Interact_performed(InputAction.CallbackContext obj) 
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
}
