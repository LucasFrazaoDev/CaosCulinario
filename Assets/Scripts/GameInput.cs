using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputActions m_playerInputActions;

    private void Awake()
    {
        Instance = this;

        m_playerInputActions = new PlayerInputActions();
        m_playerInputActions.Player.Enable();

        m_playerInputActions.Player.Interact.performed += Interact_performed;
        m_playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        m_playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        m_playerInputActions.Player.Interact.performed -= Interact_performed;
        m_playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        m_playerInputActions.Player.Pause.performed -= Pause_performed;

        m_playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = m_playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
