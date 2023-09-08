using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image m_timerImage;

    private void Update()
    {
        m_timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
