using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button m_soundEffectsButton;
    [SerializeField] private Button m_musicButton;
    [SerializeField] private Button m_closeButton;
    [SerializeField] private TextMeshProUGUI m_soundEffectsText;
    [SerializeField] private TextMeshProUGUI m_musicText;

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
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGamePaused;
        UpdateVisual();

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        m_soundEffectsText.text = "Sounds Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        m_musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
