using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter m_stoveCounter;
    private AudioSource m_audioSource;
    private float m_warningSoundTimer;
    private bool m_playWarningSound;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_stoveCounter.OnStateChanged += M_stoveCounter_OnStateChanged;
        m_stoveCounter.OnProgressChanged += M_stoveCounter_OnProgressChanged;
    }

    private void M_stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        m_playWarningSound = m_stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void M_stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(playSound )
            m_audioSource.Play();
        else
            m_audioSource.Pause();
    }

    private void Update()
    {
        if (m_playWarningSound)
        {
            m_warningSoundTimer -= Time.deltaTime;
            if (m_warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = 0.2f;
                m_warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(m_stoveCounter.transform.position);
            }
        }
    }
}
