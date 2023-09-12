using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "PopUp";

    [Header("UI Elements")]
    [SerializeField] private Image m_backgroundImage;
    [SerializeField] private TextMeshProUGUI m_messageText;
    [SerializeField] private Image m_iconImage;
    
    [Header("Sprites and colors for UI elemets")]
    [SerializeField] private Color m_successColor;
    [SerializeField] private Color m_failedColor;
    [SerializeField] private Sprite m_successSprite;
    [SerializeField] private Sprite m_failedSprite;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSucess += DeliveryManager_OnRecipeSucess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);

        m_animator.SetTrigger(POPUP);

        m_backgroundImage.color = m_failedColor;
        m_iconImage.sprite = m_failedSprite;
        m_messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSucess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);

        m_animator.SetTrigger(POPUP);

        m_backgroundImage.color = m_successColor;
        m_iconImage.sprite = m_successSprite;
        m_messageText.text = "DELIVERY\nSUCCESS";
    }
}
