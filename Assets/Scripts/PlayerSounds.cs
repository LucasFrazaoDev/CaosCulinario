using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player m_player;
    private float m_footstepTimer;
    private float m_footstepTimerMax = .1f;

    private void Awake()
    {
        m_player = GetComponent<Player>();
    }

    private void Update()
    {
        m_footstepTimer -= Time.deltaTime;
        if(m_footstepTimer < 0)
        {
            m_footstepTimer = m_footstepTimerMax;

            if (m_player.IsWalking())
            {
                float volume = 0.8f;
                SoundManager.Instance.PlayFootStepsSound(m_player.transform.position, volume);
            }
        }
    }
}
