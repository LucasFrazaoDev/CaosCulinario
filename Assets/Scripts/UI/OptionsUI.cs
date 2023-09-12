using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [Header("Menu options references")]
    [SerializeField] private Button m_soundEffectsButton;
    [SerializeField] private Button m_musicButton;
    [SerializeField] private Button m_closeButton;
    [SerializeField] private TextMeshProUGUI m_soundEffectsButtonText;
    [SerializeField] private TextMeshProUGUI m_musicButtonText;
    [SerializeField] private Transform m_pressToRebindKeyTransform;

    [Header("Button options bind references")]
    [SerializeField] private Button m_moveUpButton;
    [SerializeField] private Button m_moveDownButton;
    [SerializeField] private Button m_moveLeftButton;
    [SerializeField] private Button m_moveRightButton;
    [SerializeField] private Button m_interactButton;
    [SerializeField] private Button m_interactAlternateButton;
    [SerializeField] private Button m_pauseButton;

    [Header("Button text options bind references")]
    [SerializeField] private TextMeshProUGUI m_moveUpText;
    [SerializeField] private TextMeshProUGUI m_moveDownText;
    [SerializeField] private TextMeshProUGUI m_moveLeftText;
    [SerializeField] private TextMeshProUGUI m_moveRightText;
    [SerializeField] private TextMeshProUGUI m_interactText;
    [SerializeField] private TextMeshProUGUI m_interactAlternateText;
    [SerializeField] private TextMeshProUGUI m_pauseText;

    private void Awake()
    {
        Instance = this;

        m_soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        m_musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        m_closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        m_moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.MoveUp);
        });

        m_moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.MoveDown);
        });

        m_moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.MoveLeft);
        });

        m_moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.MoveRight);
        });

        m_interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });

        m_interactAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });

        m_pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGamePaused;
        UpdateVisual();

        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        m_soundEffectsButtonText.text = "Sounds Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        m_musicButtonText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        m_moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        m_moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        m_moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        m_moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        m_interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        m_interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        m_pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        m_pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey()
    {
        m_pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
