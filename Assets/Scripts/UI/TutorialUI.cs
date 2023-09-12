using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [Header("Keyboard keys text")]
    [SerializeField] private TextMeshProUGUI m_keyMoveUpText;
    [SerializeField] private TextMeshProUGUI m_keyMoveDownText;
    [SerializeField] private TextMeshProUGUI m_keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI m_keyMoveRightText;
    [SerializeField] private TextMeshProUGUI m_keyInteractText;
    [SerializeField] private TextMeshProUGUI m_keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI m_keyPauseText;

    [Header("Gamepad keys text")]
    [SerializeField] private TextMeshProUGUI m_keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI m_keyGamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI m_keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        UpdateVisual();
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        m_keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        m_keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        m_keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        m_keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        m_keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        m_keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        m_keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        m_keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        m_keyGamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        m_keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }
}
