using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_recipesDeliveredText;
    [SerializeField] private Button m_restartButton;
    [SerializeField] private Button m_mainMenuButton;

    private void Awake()
    {
        m_restartButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.GameScene); });
        m_mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenuScene); });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            m_recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        else
            Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        m_restartButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
