using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// Acts as a central hub for all player input. It wraps the generated Unity Input System class
/// and exposes input actions as standard events and getter methods.
/// </summary>
public class GameInput : MonoBehaviour // Author: Stefan Cwiek
{
    // Events that other scripts can subscribe to for action based inputs.
    public event EventHandler OnJumpAction;
    public event EventHandler OnHighlightAction;
    public event EventHandler OnHighlightCancel;
    public event EventHandler OnInventoryAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnExitAction;

    // Reference to the auto generated input map class
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        // Initialize the input system and enable the Player action map
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();

        // Subscribe local methods to the input system's performed callbacks.
        // This links the input detection to the event system
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Highlight.performed += Highlight_performed;
        inputActions.Player.Highlight.canceled += Highlight_cancelled;
        inputActions.Player.Inventory.performed += Inventory_performed;
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.Exit.performed += Exit_performed;
    }

    private void OnDestroy()
    {
        // unsubscribe from input events to prevent memory leaks
        inputActions.Player.Jump.performed -= Jump_performed;
        inputActions.Player.Highlight.performed -= Highlight_performed;
        inputActions.Player.Highlight.canceled -= Highlight_cancelled;
        inputActions.Player.Inventory.performed -= Inventory_performed;
        inputActions.Player.Interact.performed -= Interact_performed;
        inputActions.Player.Exit.performed -= Exit_performed;

        inputActions.Disable();
        
        inputActions.Dispose();
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }
    
    private void Highlight_performed(InputAction.CallbackContext obj)
    {
        if (NewDialogueManager.Instance.dialogueIsPlaying) return;
        OnHighlightAction?.Invoke(this, EventArgs.Empty);
    }

    private void Highlight_cancelled(InputAction.CallbackContext obj)
    {
        OnHighlightCancel?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Reads the current value of the movement keys.
    /// </summary>
    /// <returns>A normalized Vector2 representing the movement direction.</returns>
    public Vector2 GetMovementVectorNormalized()
    {
        return inputActions.Player.Move.ReadValue<Vector2>().normalized;
    }

    /// <summary>
    /// Reads the delta of the mouse for camera rotation
    /// </summary>
    /// <returns>A Vector2 representing the look delta</returns>
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

    private void Exit_performed(InputAction.CallbackContext obj) 
    {
        OnExitAction?.Invoke(this, EventArgs.Empty);
    }

    // Optional helper to read the highlight Vector2 value:
    public Vector2 GetHighlightVector()
    {
        return inputActions.Player.Highlight.ReadValue<Vector2>();
    }
}
