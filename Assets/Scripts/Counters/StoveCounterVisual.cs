using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject m_stoveOnGameObject;
    [SerializeField] private GameObject m_particlesGameObject;
    [SerializeField] private StoveCounter m_stoveCounter;

    private void Start()
    {
        m_stoveCounter.OnStateChanged += M_stoveCounter_OnStateChanged;
    }

    private void M_stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        m_stoveOnGameObject.SetActive(showVisual);
        m_particlesGameObject.SetActive(showVisual);
    }
}
