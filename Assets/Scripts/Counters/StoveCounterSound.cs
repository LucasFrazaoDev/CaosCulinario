using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter m_stoveCounter;
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_stoveCounter.OnStateChanged += M_stoveCounter_OnStateChanged;
    }

    private void M_stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(playSound )
        {
            m_audioSource.Play();
        }
        else
        {
            m_audioSource.Pause();
        }
    }
}
