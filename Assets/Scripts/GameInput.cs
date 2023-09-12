using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREF_BINDINGS = "InputBidings";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
        GamepadInteract,
        GamepadInteractAlternate,
        GamepadPause
    }

    private PlayerInputActions m_playerInputActions;

    private void Awake()
    {
        Instance = this;

        m_playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREF_BINDINGS))
        {
            m_playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREF_BINDINGS));
        }

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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                return m_playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return m_playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return m_playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return m_playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return m_playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return m_playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return m_playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.GamepadInteract:
                return m_playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case Binding.GamepadInteractAlternate:
                return m_playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.GamepadPause:
                return m_playerInputActions.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        m_playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.MoveUp:
                inputAction = m_playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = m_playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = m_playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = m_playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = m_playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = m_playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = m_playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.GamepadInteract:
                inputAction = m_playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.GamepadInteractAlternate:
                inputAction = m_playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.GamepadPause:
                inputAction = m_playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            m_playerInputActions.Player.Enable();
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREF_BINDINGS, m_playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }
}
